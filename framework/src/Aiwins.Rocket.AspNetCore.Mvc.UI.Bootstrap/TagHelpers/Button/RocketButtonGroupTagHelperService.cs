using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    public class RocketButtonGroupTagHelperService : RocketTagHelperService<RocketButtonGroupTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            AddButtonGroupClass(context, output);
            AddSizeClass(context, output);
            AddAttributes(context, output);
        }

        protected virtual void AddSizeClass(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Size)
            {
                case RocketButtonGroupSize.Default:
                    break;
                case RocketButtonGroupSize.Small:
                    output.Attributes.AddClass("btn-group-sm");
                    break;
                case RocketButtonGroupSize.Medium:
                    output.Attributes.AddClass("btn-group-md");
                    break;
                case RocketButtonGroupSize.Large:
                    output.Attributes.AddClass("btn-group-lg");
                    break;
            }
        }

        protected virtual void AddButtonGroupClass(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Direction)
            {
                case RocketButtonGroupDirection.Horizontal:
                    output.Attributes.AddClass("btn-group");
                    break;
                case RocketButtonGroupDirection.Vertical:
                    output.Attributes.AddClass("btn-group-vertical");
                    break;
                default:
                    output.Attributes.AddClass("btn-group");
                    break;
            }
        }

        protected virtual void AddAttributes(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("role", "group");
        }
    }
}