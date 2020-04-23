using System;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RocketRadioButton : Attribute
    {
        public bool Inline { get; set; } = false;

        public bool Disabled { get; set; } = false;
    }
}
