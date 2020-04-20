﻿using System.Net.Http;

namespace Aiwins.Rocket.Http.Client.Authentication {
    public class RemoteServiceHttpClientAuthenticateContext {
        public HttpClient Client { get; }

        public HttpRequestMessage Request { get; }

        public RemoteServiceConfiguration RemoteService { get; }

        public string RemoteServiceName { get; }

        public RemoteServiceHttpClientAuthenticateContext (
            HttpClient client,
            HttpRequestMessage request,
            RemoteServiceConfiguration remoteService,
            string remoteServiceName) {
            Client = client;
            Request = request;
            RemoteService = remoteService;
            RemoteServiceName = remoteServiceName;
        }
    }
}