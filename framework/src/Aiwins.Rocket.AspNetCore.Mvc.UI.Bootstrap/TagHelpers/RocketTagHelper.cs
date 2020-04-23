using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers
{
    public abstract class RocketTagHelper : TagHelper, ITransientDependency
    {
        
    }

    public abstract class RocketTagHelper<TTagHelper, TService> : RocketTagHelper
        where TTagHelper : RocketTagHelper<TTagHelper, TService>
        where TService : class, IRocketTagHelperService<TTagHelper> 
    {
        protected TService Service { get; }

        public override int Order => Service.Order;

        protected RocketTagHelper(TService service)
        {
            Service = service;
            Service.As<RocketTagHelperService<TTagHelper>>().TagHelper = (TTagHelper)this;
        }

        public override void Init(TagHelperContext context)
        {
            Service.Init(context);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Service.Process(context, output);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return Service.ProcessAsync(context, output);
        }
    }
}