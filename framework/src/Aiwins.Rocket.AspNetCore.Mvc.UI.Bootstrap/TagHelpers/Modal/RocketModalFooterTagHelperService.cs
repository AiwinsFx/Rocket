using System.Text;
using Localization.Resources.RocketUi;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    public class RocketModalFooterTagHelperService : RocketTagHelperService<RocketModalFooterTagHelper>
    {
        private readonly IStringLocalizer<RocketUiResource> _localizer;

        public RocketModalFooterTagHelperService(IStringLocalizer<RocketUiResource> localizer)
        {
            _localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.AddClass("modal-footer");

            if (TagHelper.Buttons != RocketModalButtons.None)
            {
                output.PostContent.SetHtmlContent(CreateContent());
            }
            ProcessButtonsAlignment(output);
        }

        protected virtual string CreateContent()
        {
            var sb = new StringBuilder();

            switch (TagHelper.Buttons)
            {
                case RocketModalButtons.Cancel:
                    sb.AppendLine("<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">" + _localizer["Cancel"] + "</button>");
                    break;
                case RocketModalButtons.Close:
                    sb.AppendLine("<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">" + _localizer["Close"] + "</button>");
                    break;
                case RocketModalButtons.Save:
                    sb.AppendLine("<button type=\"submit\" class=\"btn btn-primary\" data-busy-text=\"" + _localizer["SavingWithThreeDot"] + "\"><i class=\"fa fa-check\"></i> <span>" + _localizer["Save"] + "</span></button>");
                    break;
                case RocketModalButtons.Save | RocketModalButtons.Cancel:
                    sb.AppendLine("<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">" + _localizer["Cancel"] + "</button>");
                    sb.AppendLine("<button type=\"submit\" class=\"btn btn-primary\" data-busy-text=\"" + _localizer["SavingWithThreeDot"] + "\"><i class=\"fa fa-check\"></i> <span>" + _localizer["Save"] + "</span></button>");
                    break;
                case RocketModalButtons.Save | RocketModalButtons.Close:
                    sb.AppendLine("<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">" + _localizer["Close"] + "</button>");
                    sb.AppendLine("<button type=\"submit\" class=\"btn btn-primary\" data-busy-text=\"" + _localizer["SavingWithThreeDot"] + "\"><i class=\"fa fa-check\"></i> <span>" + _localizer["Save"] + "</span></button>");
                    break;
            }

            return sb.ToString();
        }

        protected virtual void ProcessButtonsAlignment(TagHelperOutput output)
        {
            if (TagHelper.ButtonAlignment == ButtonsAlign.Default)
            {
                return;
            }
            output.Attributes.AddClass("justify-content-" + TagHelper.ButtonAlignment.ToString().ToLowerInvariant());
        }
    }
}