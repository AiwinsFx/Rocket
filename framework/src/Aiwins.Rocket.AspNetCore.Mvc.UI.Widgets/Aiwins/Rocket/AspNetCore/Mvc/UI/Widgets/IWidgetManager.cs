using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets
{
    public interface IWidgetManager : ITransientDependency
    {
        Task<bool> IsGrantedAsync(Type widgetComponentType);

        Task<bool> IsGrantedAsync(string name);
    }
}
