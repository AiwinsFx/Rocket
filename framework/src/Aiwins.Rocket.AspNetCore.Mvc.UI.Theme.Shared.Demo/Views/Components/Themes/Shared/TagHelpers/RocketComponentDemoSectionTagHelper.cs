using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.FileProviders;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.TagHelpers
{
    public class RocketComponentDemoSectionTagHelper : RocketTagHelper
    {
        private const string DemoSectionOpeningTag = "<rocket-component-demo-section";
        private const string DemoSectionClosingTag = "</rocket-component-demo-section";

        public string ViewPath { get; set; }
        public string Title { get; set; }

        private readonly IVirtualFileProvider _virtualFileProvider;

        public RocketComponentDemoSectionTagHelper(IVirtualFileProvider virtualFileProvider)
        {
            _virtualFileProvider = virtualFileProvider;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            var content = await output.GetChildContentAsync();

            output.PreContent.AppendHtml("<div class=\"rocket-component-demo-section\">");
            output.PreContent.AppendHtml($"<h2>{Title}</h2>");
            output.PreContent.AppendHtml("<div class=\"rocket-component-demo-section-body\">");
            /* component rendering here */
            output.PostContent.AppendHtml("</div>"); //rocket-component-demo-section-body
            AppendRawSource(output);
            AppendBootstrapSource(output, content);
            output.PostContent.AppendHtml("</div>"); //rocket-component-demo-section
        }

        private static void AppendBootstrapSource(TagHelperOutput output, TagHelperContent content)
        {
            output.PostContent.AppendHtml("<div class=\"rocket-component-demo-section-bs-source\">");
            output.PostContent.AppendHtml("<h3>Bootstrap</h3>");
            output.PostContent.AppendHtml("<pre>");
            output.PostContent.Append(content.GetContent());
            output.PostContent.AppendHtml("</pre>");
            output.PostContent.AppendHtml("</div>");
        }

        private void AppendRawSource(TagHelperOutput output)
        {
            output.PostContent.AppendHtml("<div class=\"rocket-component-demo-section-raw-source\">");
            output.PostContent.AppendHtml("<h3>ROCKET Tag Helpers</h3>");
            output.PostContent.AppendHtml("<pre>");
            output.PostContent.Append(GetRawDemoSource());
            output.PostContent.AppendHtml("</pre>");
            output.PostContent.AppendHtml("</div>");
        }

        private string GetRawDemoSource()
        {
            StringBuilder sourceBuilder = null;

            var lines = GetFileContent().SplitToLines();

            foreach (var line in lines)
            {
                if (line.Contains(DemoSectionOpeningTag) && GetName(line) == Title)
                {
                    sourceBuilder = new StringBuilder();
                }
                else if (line.Contains(DemoSectionClosingTag, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sourceBuilder == null)
                    {
                        continue;
                    }

                    return sourceBuilder.ToString();
                }
                else if (sourceBuilder != null)
                {
                    sourceBuilder.AppendLine(line);
                }
            }

            throw new RocketException($"Could not find {Title} demo section inside {ViewPath}");
        }

        private string GetFileContent()
        {
            var viewFileInfo = _virtualFileProvider.GetFileInfo(ViewPath);
            return viewFileInfo.ReadAsString();
        }

        private string GetName(string line)
        {
            var str = line.Substring(line.IndexOf("title=\"", StringComparison.OrdinalIgnoreCase) + "title=\"".Length);
            str = str.Left(str.IndexOf("\"", StringComparison.OrdinalIgnoreCase));
            return str;
        }
    }
}
