using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Layout
{
    public class PageLayout : IPageLayout, IScopedDependency
    {
        public ContentLayout Content { get; }

        public PageLayout()
        {
            Content = new ContentLayout();
        }
    }
}