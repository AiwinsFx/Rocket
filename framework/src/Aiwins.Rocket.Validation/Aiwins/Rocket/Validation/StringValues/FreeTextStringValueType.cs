using System;

namespace Aiwins.Rocket.Validation.StringValues {
    [Serializable]
    [StringValueType ("FREE_TEXT")]
    public class FreeTextStringValueType : StringValueTypeBase {
        public FreeTextStringValueType () {

        }

        public FreeTextStringValueType (IValueValidator validator) : base (validator) { }
    }
}