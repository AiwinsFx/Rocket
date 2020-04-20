using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.Validation
{
    public class RocketValidationResult : IRocketValidationResult
    {
        public List<ValidationResult> Errors { get; }

        public RocketValidationResult()
        {
            Errors = new List<ValidationResult>();
        }
    }
}