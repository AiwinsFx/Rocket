using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers
{
    public interface IRocketTagHelperService<TTagHelper> : ITransientDependency
        where TTagHelper : TagHelper
    {
        TTagHelper TagHelper { get; }

        int Order { get; }

        void Init(TagHelperContext context);

        void Process(TagHelperContext context, TagHelperOutput output);

        Task ProcessAsync(TagHelperContext context, TagHelperOutput output);
    }
}