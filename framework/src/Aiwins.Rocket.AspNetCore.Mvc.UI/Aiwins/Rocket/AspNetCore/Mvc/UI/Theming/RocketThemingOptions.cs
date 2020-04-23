namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theming
{
    public class RocketThemingOptions
    {
        public ThemeDictionary Themes { get; }

        public string DefaultThemeName { get; set; }

        public RocketThemingOptions()
        {
            Themes = new ThemeDictionary();
        }
    }
}
