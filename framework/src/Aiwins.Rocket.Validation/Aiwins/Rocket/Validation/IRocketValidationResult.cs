using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.Validation {
    public interface IRocketValidationResult {
        List<ValidationResult> Errors { get; }
    }
}