﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;
using Aiwins.Rocket.Http.Client.Authentication;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Http.ProxyScripting.Generators;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Reflection;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.Tracing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public class DynamicHttpProxyInterceptor<TService> : RocketInterceptor, ITransientDependency {
        // ReSharper disable once StaticMemberInGenericType
        protected static MethodInfo GenericInterceptAsyncMethod { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }
        protected ICorrelationIdProvider CorrelationIdProvider { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected RocketCorrelationIdOptions RocketCorrelationIdOptions { get; }
        protected IDynamicProxyHttpClientFactory HttpClientFactory { get; }
        protected IApiDescriptionFinder ApiDescriptionFinder { get; }
        protected RocketRemoteServiceOptions RocketRemoteServiceOptions { get; }
        protected RocketHttpClientOptions ClientOptions { get; }
        protected IJsonSerializer JsonSerializer { get; }
        protected IRemoteServiceHttpClientAuthenticator ClientAuthenticator { get; }

        public ILogger<DynamicHttpProxyInterceptor<TService>> Logger { get; set; }

        static DynamicHttpProxyInterceptor () {
            GenericInterceptAsyncMethod = typeof (DynamicHttpProxyInterceptor<TService>)
                .GetMethods (BindingFlags.NonPublic | BindingFlags.Instance)
                .First (m => m.Name == nameof (MakeRequestAndGetResultAsync) && m.IsGenericMethodDefinition);
        }

        public DynamicHttpProxyInterceptor (
            IDynamicProxyHttpClientFactory httpClientFactory,
            IOptions<RocketHttpClientOptions> clientOptions,
            IOptionsSnapshot<RocketRemoteServiceOptions> remoteServiceOptions,
            IApiDescriptionFinder apiDescriptionFinder,
            IJsonSerializer jsonSerializer,
            IRemoteServiceHttpClientAuthenticator clientAuthenticator,
            ICancellationTokenProvider cancellationTokenProvider,
            ICorrelationIdProvider correlationIdProvider,
            IOptions<RocketCorrelationIdOptions> correlationIdOptions,
            ICurrentTenant currentTenant) {
            CancellationTokenProvider = cancellationTokenProvider;
            CorrelationIdProvider = correlationIdProvider;
            CurrentTenant = currentTenant;
            RocketCorrelationIdOptions = correlationIdOptions.Value;
            HttpClientFactory = httpClientFactory;
            ApiDescriptionFinder = apiDescriptionFinder;
            JsonSerializer = jsonSerializer;
            ClientAuthenticator = clientAuthenticator;
            ClientOptions = clientOptions.Value;
            RocketRemoteServiceOptions = remoteServiceOptions.Value;

            Logger = NullLogger<DynamicHttpProxyInterceptor<TService>>.Instance;
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            if (invocation.Method.ReturnType.GenericTypeArguments.IsNullOrEmpty ()) {
                await MakeRequestAsync (invocation);
            } else {
                var result = (Task) GenericInterceptAsyncMethod
                    .MakeGenericMethod (invocation.Method.ReturnType.GenericTypeArguments[0])
                    .Invoke (this, new object[] { invocation });

                invocation.ReturnValue = await GetResultAsync (
                    result,
                    invocation.Method.ReturnType.GetGenericArguments () [0]
                );
            }

        }

        private async Task<object> GetResultAsync (Task task, Type resultType) {
            await task;
            return typeof (Task<>)
                .MakeGenericType (resultType)
                .GetProperty (nameof (Task<object>.Result), BindingFlags.Instance | BindingFlags.Public)
                .GetValue (task);
        }

        private async Task<T> MakeRequestAndGetResultAsync<T> (IRocketMethodInvocation invocation) {
            var responseAsString = await MakeRequestAsync(invocation);
            
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(responseAsString, typeof(T));
            }

            return JsonSerializer.Deserialize<T>(responseAsString);
        }

        private async Task<string> MakeRequestAsync (IRocketMethodInvocation invocation) {
            var clientConfig = ClientOptions.HttpClientProxies.GetOrDefault (typeof (TService)) ??
                throw new RocketException ($"Could not get DynamicHttpClientProxyConfig for {typeof(TService).FullName}.");
            var remoteServiceConfig = RocketRemoteServiceOptions.RemoteServices.GetConfigurationOrDefault (clientConfig.RemoteServiceName);

            var client = HttpClientFactory.Create (clientConfig.RemoteServiceName);

            var action = await ApiDescriptionFinder.FindActionAsync (remoteServiceConfig.BaseUrl, typeof (TService), invocation.Method);
            var apiVersion = GetApiVersionInfo (action);
            var url = remoteServiceConfig.BaseUrl.EnsureEndsWith ('/') + UrlBuilder.GenerateUrlWithParameters (action, invocation.ArgumentsDictionary, apiVersion);

            var requestMessage = new HttpRequestMessage (action.GetHttpMethod (), url) {
                Content = RequestPayloadBuilder.BuildContent (action, invocation.ArgumentsDictionary, JsonSerializer, apiVersion)
            };

            AddHeaders (invocation, action, requestMessage, apiVersion);

            await ClientAuthenticator.AuthenticateAsync (
                new RemoteServiceHttpClientAuthenticateContext (
                    client,
                    requestMessage,
                    remoteServiceConfig,
                    clientConfig.RemoteServiceName
                )
            );

            var response = await client.SendAsync (requestMessage, GetCancellationToken ());

            if (!response.IsSuccessStatusCode) {
                await ThrowExceptionForResponseAsync (response);
            }

            return await response.Content.ReadAsStringAsync ();
        }

        private ApiVersionInfo GetApiVersionInfo (ActionApiDescriptionModel action) {
            var apiVersion = FindBestApiVersion (action);

            //TODO: Make names configurable?
            var versionParam = action.Parameters.FirstOrDefault (p => p.Name == "apiVersion" && p.BindingSourceId == ParameterBindingSources.Path) ??
                action.Parameters.FirstOrDefault (p => p.Name == "api-version" && p.BindingSourceId == ParameterBindingSources.Query);

            return new ApiVersionInfo (versionParam?.BindingSourceId, apiVersion);
        }

        private string FindBestApiVersion (ActionApiDescriptionModel action) {
            var configuredVersion = GetConfiguredApiVersion ();

            if (action.SupportedVersions.IsNullOrEmpty ()) {
                return configuredVersion ?? "1.0";
            }

            if (action.SupportedVersions.Contains (configuredVersion)) {
                return configuredVersion;
            }

            return action.SupportedVersions.Last (); //TODO: Ensure to get the latest version!
        }

        protected virtual void AddHeaders (IRocketMethodInvocation invocation, ActionApiDescriptionModel action, HttpRequestMessage requestMessage, ApiVersionInfo apiVersion) {
            //API Version
            if (!apiVersion.Version.IsNullOrEmpty ()) {
                //TODO: What about other media types?
                requestMessage.Headers.Add ("accept", $"{MimeTypes.Text.Plain}; v={apiVersion.Version}");
                requestMessage.Headers.Add ("accept", $"{MimeTypes.Application.Json}; v={apiVersion.Version}");
                requestMessage.Headers.Add ("api-version", apiVersion.Version);
            }

            //Header parameters
            var headers = action.Parameters.Where (p => p.BindingSourceId == ParameterBindingSources.Header).ToArray ();
            foreach (var headerParameter in headers) {
                var value = HttpActionParameterHelper.FindParameterValue (invocation.ArgumentsDictionary, headerParameter);
                if (value != null) {
                    requestMessage.Headers.Add (headerParameter.Name, value.ToString ());
                }
            }

            //CorrelationId
            requestMessage.Headers.Add (RocketCorrelationIdOptions.HttpHeaderName, CorrelationIdProvider.Get ());

            //TenantId
            if (CurrentTenant.Id.HasValue) {
                //TODO: Use RocketAspNetCoreMultiTenancyOptions to get the key
                requestMessage.Headers.Add (TenantResolverConsts.DefaultTenantKey, CurrentTenant.Id.Value.ToString ());
            }

            //Culture
            //TODO: Is that the way we want? Couldn't send the culture (not ui culture)
            var currentCulture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
            if (!currentCulture.IsNullOrEmpty ()) {
                requestMessage.Headers.AcceptLanguage.Add (new StringWithQualityHeaderValue (currentCulture));
            }

            //X-Requested-With
            requestMessage.Headers.Add ("X-Requested-With", "XMLHttpRequest");
        }

        private string GetConfiguredApiVersion () {
            var clientConfig = ClientOptions.HttpClientProxies.GetOrDefault (typeof (TService)) ??
                throw new RocketException ($"Could not get DynamicHttpClientProxyConfig for {typeof(TService).FullName}.");

            return RocketRemoteServiceOptions.RemoteServices.GetOrDefault (clientConfig.RemoteServiceName)?.Version ??
                RocketRemoteServiceOptions.RemoteServices.Default?.Version;
        }

        private async Task ThrowExceptionForResponseAsync (HttpResponseMessage response) {
            if (response.Headers.Contains (RocketHttpConsts.RocketErrorFormat)) {
                var errorResponse = JsonSerializer.Deserialize<RemoteServiceErrorResponse> (
                    await response.Content.ReadAsStringAsync ()
                );

                throw new RocketRemoteCallException (errorResponse.Error);
            }

            throw new RocketException ($"Remote service returns error! HttpStatusCode: {response.StatusCode}, ReasonPhrase: {response.ReasonPhrase}");
        }

        protected virtual CancellationToken GetCancellationToken () {
            return CancellationTokenProvider.Token;
        }
    }
}