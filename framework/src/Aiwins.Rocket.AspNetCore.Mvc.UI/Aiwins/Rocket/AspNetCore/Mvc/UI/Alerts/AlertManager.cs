using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Alerts
{
    public class AlertManager : IAlertManager, IScopedDependency
    {
        public AlertList Alerts { get; }

        public AlertManager()
        {
            Alerts = new AlertList();
        }
    }
}