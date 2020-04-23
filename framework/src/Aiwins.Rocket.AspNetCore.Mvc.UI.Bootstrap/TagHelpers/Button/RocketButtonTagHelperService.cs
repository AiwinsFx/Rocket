using System;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    public class RocketButtonTagHelperService : RocketButtonTagHelperServiceBase<RocketButtonTagHelper>
    {
        protected const string DataBusyTextAttributeName = "data-busy-text";

        protected IStringLocalizer<RocketUiResource> L { get; }

        public RocketButtonTagHelperService(IStringLocalizer<RocketUiResource> localizer)
        {
            L = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "button";
            AddType(context, output);
            AddBusyText(context, output);
        }

        protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes.ContainsName("type"))
            {
                return;
            }

            output.Attributes.Add("type", "button");
        }

        protected virtual void AddBusyText(TagHelperContext context, TagHelperOutput output)
        {
            var busyText = TagHelper.BusyText ?? L["ProcessingWithThreeDot"];
            if (busyText.IsNullOrWhiteSpace())
            {
                return;
            }

            output.Attributes.SetAttribute(DataBusyTextAttributeName, busyText);
        }
    }
}