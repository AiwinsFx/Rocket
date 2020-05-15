using System;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending {
    public interface IExtensionPropertyAttributeDtoFactory {
        ExtensionPropertyAttributeDto Create (Attribute attribute);
    }
}