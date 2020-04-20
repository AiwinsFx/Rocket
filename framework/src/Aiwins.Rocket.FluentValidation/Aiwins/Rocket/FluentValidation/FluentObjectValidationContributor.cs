using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Validation;

namespace Aiwins.Rocket.FluentValidation {
    public class FluentObjectValidationContributor : IObjectValidationContributor, ITransientDependency {
        private readonly IServiceProvider _serviceProvider;

        public FluentObjectValidationContributor (
            IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public void AddErrors (ObjectValidationContext context) {
            var serviceType = typeof (IValidator<>).MakeGenericType (context.ValidatingObject.GetType ());
            var validator = _serviceProvider.GetService (serviceType) as IValidator;
            if (validator == null) {
                return;
            }

            var result = validator.Validate (context.ValidatingObject);
            if (!result.IsValid) {
                context.Errors.AddRange (
                    result.Errors.Select (
                        error =>
                        new ValidationResult (error.ErrorMessage, new [] { error.PropertyName })
                    )
                );
            }
        }
    }
}