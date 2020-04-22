using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.ModelBinding.Metadata
{
    [Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
    [ExposeServices(typeof(IModelMetadataProvider))]
    public class RocketModelMetadataProvider : DefaultModelMetadataProvider
    {
        private static readonly PropertyInfo ValidationAttributeErrorMessageStringProperty;

        static RocketModelMetadataProvider()
        {
            ValidationAttributeErrorMessageStringProperty = typeof(ValidationAttribute)
                .GetProperty("ErrorMessageString", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public RocketModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider)
            : base(detailsProvider)
        {
        }

        public RocketModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider, IOptions<MvcOptions> optionsAccessor)
            : base(detailsProvider, optionsAccessor)
        {
        }

        protected override DefaultMetadataDetails[] CreatePropertyDetails(ModelMetadataIdentity key)
        {
            var details = base.CreatePropertyDetails(key);

            foreach (var detail in details)
            {
                NormalizeMetadataDetail(detail);
            }

            return details;
        }

        private void NormalizeMetadataDetail(DefaultMetadataDetails detail)
        {
            foreach (var validationAttribute in detail.ModelAttributes.Attributes.OfType<ValidationAttribute>())
            {
                NormalizeValidationAttrbute(validationAttribute);
            }
        }

        protected virtual void NormalizeValidationAttrbute(ValidationAttribute validationAttribute)
        {
            if (validationAttribute.ErrorMessage == null)
            {
                validationAttribute.ErrorMessage = ValidationAttributeErrorMessageStringProperty.GetValue(validationAttribute) as string;
            }
        }
    }
}