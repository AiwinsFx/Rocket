using System;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormControlSize : Attribute
    {
        public RocketFormControlSize Size { get; set; }

        public FormControlSize(RocketFormControlSize size)
        {
            Size = size;
        }
    }
}
