using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiwins.Rocket.AspNetCore.Mvc.Validation {
    public class RocketValidationActionFilter : IAsyncActionFilter, ITransientDependency {
        private readonly IModelStateValidator _validator;

        public RocketValidationActionFilter (IModelStateValidator validator) {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            //TODO: 考虑如何实现控制器禁用验证?

            if (!context.ActionDescriptor.IsControllerAction () ||
                !context.ActionDescriptor.HasObjectResult ()) {
                await next ();
                return;
            }

            _validator.Validate (context.ModelState);
            await next ();
        }
    }
}