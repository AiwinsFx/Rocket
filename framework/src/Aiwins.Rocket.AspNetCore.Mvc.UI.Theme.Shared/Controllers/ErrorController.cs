using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.ExceptionHandling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Views.Error;
using Aiwins.Rocket.ExceptionHandling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Controllers
{
    public class ErrorController : RocketController
    {
        private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
        private readonly IStringLocalizer<RocketUiResource> _localizer;
        private readonly RocketErrorPageOptions _abpErrorPageOptions;
        private readonly IExceptionNotifier _exceptionNotifier;

        public ErrorController(
            IExceptionToErrorInfoConverter exceptionToErrorInfoConverter,
            IHttpExceptionStatusCodeFinder httpExceptionStatusCodeFinder,
            IOptions<RocketErrorPageOptions> abpErrorPageOptions,
            IStringLocalizer<RocketUiResource> localizer, 
            IExceptionNotifier exceptionNotifier)
        {
            _errorInfoConverter = exceptionToErrorInfoConverter;
            _statusCodeFinder = httpExceptionStatusCodeFinder;
            _localizer = localizer;
            _exceptionNotifier = exceptionNotifier;
            _abpErrorPageOptions = abpErrorPageOptions.Value;
        }

        public async Task<IActionResult> Index(int httpStatusCode)
        {
            var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = exHandlerFeature != null
                ? exHandlerFeature.Error
                : new Exception(_localizer["UnhandledException"]);

            await _exceptionNotifier.NotifyAsync(new ExceptionNotificationContext(exception));

            var errorInfo = _errorInfoConverter.Convert(exception);

            if (httpStatusCode == 0)
            {
                httpStatusCode = (int)_statusCodeFinder.GetStatusCode(HttpContext, exception);
            }

            HttpContext.Response.StatusCode = httpStatusCode;

            var page = GetErrorPageUrl(httpStatusCode);

            return View(page, new RocketErrorViewModel
            {
                ErrorInfo = errorInfo,
                HttpStatusCode = httpStatusCode
            });
        }

        private string GetErrorPageUrl(int statusCode)
        {
            var page = _abpErrorPageOptions.ErrorViewUrls.GetOrDefault(statusCode.ToString());

            if (string.IsNullOrWhiteSpace(page))
            {
                return "~/Views/Error/Default.cshtml";
            }

            return page;
        }
    }
}
