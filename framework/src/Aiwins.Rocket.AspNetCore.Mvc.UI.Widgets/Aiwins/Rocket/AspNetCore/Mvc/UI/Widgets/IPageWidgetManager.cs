using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets
{
    public interface IPageWidgetManager
    {
        bool TryAdd(WidgetDefinition widget);

        IReadOnlyList<WidgetDefinition> GetAll();
    }
}