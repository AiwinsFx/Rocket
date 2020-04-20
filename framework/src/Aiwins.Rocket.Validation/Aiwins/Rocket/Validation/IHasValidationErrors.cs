using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.Validation
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}