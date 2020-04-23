using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Components.LayoutHook
{
    public class LayoutHookViewComponent : RocketViewComponent
    {
        protected RocketLayoutHookOptions Options { get; }

        public LayoutHookViewComponent(IOptions<RocketLayoutHookOptions> options)
        {
            Options = options.Value;
        }

        public virtual IViewComponentResult Invoke(string name, string layout)
        {
            var hooks = Options.Hooks.GetOrDefault(name)?.ToArray() ?? Array.Empty<LayoutHookInfo>();

            return View(
                "~/Aiwins/Rocket/AspNetCore/Mvc/UI/Components/LayoutHook/Default.cshtml",
                new LayoutHookViewModel(hooks, layout)
            );
        }
    }
}
