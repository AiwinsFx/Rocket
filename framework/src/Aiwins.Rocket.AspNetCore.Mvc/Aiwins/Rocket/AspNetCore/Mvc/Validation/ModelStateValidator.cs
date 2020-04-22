using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aiwins.Rocket.AspNetCore.Mvc.Validation {
    public class ModelStateValidator : IModelStateValidator, ITransientDependency {
        public virtual void Validate (ModelStateDictionary modelState) {
            var validationResult = new RocketValidationResult ();

            AddErrors (validationResult, modelState);

            if (validationResult.Errors.Any ()) {
                throw new RocketValidationException (
                    "ModelState is not valid! See ValidationErrors for details.",
                    validationResult.Errors
                );
            }
        }

        public virtual void AddErrors (IRocketValidationResult validationResult, ModelStateDictionary modelState) {
            if (modelState.IsValid) {
                return;
            }

            foreach (var state in modelState) {
                foreach (var error in state.Value.Errors) {
                    validationResult.Errors.Add (new ValidationResult (error.ErrorMessage, new [] { state.Key }));
                }
            }
        }
    }
}