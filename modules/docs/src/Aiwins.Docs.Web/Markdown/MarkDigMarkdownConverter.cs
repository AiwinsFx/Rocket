using System.Text;
using Markdig;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Docs.Markdown
{
    public class MarkDigMarkdownConverter : IMarkdownConverter, ISingletonDependency
    {
        readonly MarkdownPipeline _markdownPipeline;

        public MarkDigMarkdownConverter()
        {
            _markdownPipeline = new MarkdownPipelineBuilder()
              .UseAutoLinks()
              .UseBootstrap()
              .UseGridTables()
              .UsePipeTables()
              .Build();
        }

        public virtual string ConvertToHtml(string markdown)
        {
            return Markdig.Markdown.ToHtml(Encoding.UTF8.GetString(Encoding.Default.GetBytes(markdown)),
                _markdownPipeline);
        }
    }
}