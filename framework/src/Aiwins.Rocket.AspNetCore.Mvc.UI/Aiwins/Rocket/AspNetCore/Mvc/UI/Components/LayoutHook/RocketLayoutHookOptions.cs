using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Components.LayoutHook
{
    public class RocketLayoutHookOptions
    {
        public IDictionary<string, List<LayoutHookInfo>> Hooks { get; }

        public RocketLayoutHookOptions()
        {
            Hooks = new Dictionary<string, List<LayoutHookInfo>>();
        }

        public RocketLayoutHookOptions Add(string name, Type componentType, string layout = null)
        {
            Hooks
                .GetOrAdd(name, () => new List<LayoutHookInfo>())
                .Add(new LayoutHookInfo(componentType, layout));

            return this;
        }
    }
}