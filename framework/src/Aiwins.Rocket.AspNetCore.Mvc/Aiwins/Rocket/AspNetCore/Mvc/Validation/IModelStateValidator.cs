using Aiwins.Rocket.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aiwins.Rocket.AspNetCore.Mvc.Validation {
    public interface IModelStateValidator {
        void Validate (ModelStateDictionary modelState);

        void AddErrors (IRocketValidationResult validationResult, ModelStateDictionary modelState);
    }
}