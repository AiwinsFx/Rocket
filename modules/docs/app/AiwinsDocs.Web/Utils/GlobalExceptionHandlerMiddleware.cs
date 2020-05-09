using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aiwins.Rocket.AspNetCore.Uow;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Docs;

namespace AiwinsDocs.Web.Utils
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RocketUnitOfWorkMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<RocketUnitOfWorkMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Handled a global exception: " + ex.Message, ex);

                if (ex.Message.StartsWith("404 error") ||
                    ex is EntityNotFoundException ||
                    ex is DocumentNotFoundException)
                {
                    httpContext.Response.Redirect("/error/404");
                }
                else
                {
                    httpContext.Response.Redirect("/error/500");
                }
            }
        }
    }
}