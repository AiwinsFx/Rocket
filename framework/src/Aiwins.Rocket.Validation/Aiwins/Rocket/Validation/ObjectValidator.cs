using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Validation {
    public class ObjectValidator : IObjectValidator, ITransientDependency {
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }
        protected RocketValidationOptions Options { get; }

        public ObjectValidator (IOptions<RocketValidationOptions> options, IHybridServiceScopeFactory serviceScopeFactory) {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        public virtual void Validate (object validatingObject, string name = null, bool allowNull = false) {
            var errors = GetErrors (validatingObject, name, allowNull);

            if (errors.Any ()) {
                throw new RocketValidationException (
                    "Object state is not valid! See ValidationErrors for details.",
                    errors
                );
            }
        }

        public virtual List<ValidationResult> GetErrors (object validatingObject, string name = null, bool allowNull = false) {
            if (validatingObject == null) {
                if (allowNull) {
                    return new List<ValidationResult> (); //TODO: 考虑返回一个验证结果的数组
                } else {
                    return new List<ValidationResult> {
                        name == null ?
                        new ValidationResult ("Given object is null!") :
                            new ValidationResult (name + " is null!", new [] { name })
                    };
                }
            }

            var context = new ObjectValidationContext (validatingObject);

            using (var scope = ServiceScopeFactory.CreateScope ()) {
                foreach (var contributorType in Options.ObjectValidationContributors) {
                    var contributor = (IObjectValidationContributor)
                    scope.ServiceProvider.GetRequiredService (contributorType);
                    contributor.AddErrors (context);
                }
            }

            return context.Errors;
        }
    }
}