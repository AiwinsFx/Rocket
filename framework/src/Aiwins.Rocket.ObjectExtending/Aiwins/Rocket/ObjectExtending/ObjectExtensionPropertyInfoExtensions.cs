using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aiwins.Rocket.ObjectExtending {
    public static class ObjectExtensionPropertyInfoExtensions {
        public static ValidationAttribute[] GetValidationAttributes (this ObjectExtensionPropertyInfo propertyInfo) {
            // return propertyInfo
            //     .Attributes
            //     .OfType<ValidationAttribute> ()
            //     .Union (propertyInfo.ValidationAttributes)
            //     .ToArray ();
            return propertyInfo.Attributes.OfType<ValidationAttribute> ().ToArray();
        }
    }
}