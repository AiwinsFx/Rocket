using System.Net;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiwins.Rocket.AspNetCore.Mvc.Response {
    public class RocketNoContentActionFilter : IAsyncActionFilter, ITransientDependency {
        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!context.ActionDescriptor.IsControllerAction ()) {
                await next ();
                return;
            }

            await next ();

            if (context.HttpContext.Response.StatusCode == (int) HttpStatusCode.OK) {
                var returnType = context.ActionDescriptor.GetReturnType ();
                if (returnType == typeof (Task) || returnType == typeof (void)) {
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NoContent;
                }
            }
        }
    }
}