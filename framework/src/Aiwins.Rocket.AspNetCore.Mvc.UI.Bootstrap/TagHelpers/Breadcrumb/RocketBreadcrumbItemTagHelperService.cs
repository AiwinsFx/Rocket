using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Breadcrumb
{
    public class RocketBreadcrumbItemTagHelperService : RocketTagHelperService<RocketBreadcrumbItemTagHelper>
    {
        private readonly HtmlEncoder _encoder;

        public RocketBreadcrumbItemTagHelperService(HtmlEncoder encoder)
        {
            _encoder = encoder;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.AddClass("breadcrumb-item");
            output.Attributes.AddClass(RocketBreadcrumbItemActivePlaceholder);

            var list = context.GetValue<List<BreadcrumbItem>>(BreadcrumbItemsContent);

            output.Content.SetHtmlContent(GetInnerHtml(context, output));
            
            list.Add(new BreadcrumbItem
            {
                Html = output.Render(_encoder),
                Active = TagHelper.Active
            });

            output.SuppressOutput();
        }

        protected virtual string GetInnerHtml(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(TagHelper.Href))
            {
                output.Attributes.Add("aria-current", "page");
                return TagHelper.Title;
            }
            return "<a href=\"" + TagHelper.Href + "\">" + TagHelper.Title + "</a>";
        }

    }
}