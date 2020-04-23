namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets
{
    public class RocketWidgetOptions
    {
        public WidgetDefinitionCollection Widgets { get; }

        public RocketWidgetOptions()
        {
            Widgets = new WidgetDefinitionCollection();
        }
    }
}
