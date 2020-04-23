namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    public interface IButtonTagHelperBase
    {
        RocketButtonType ButtonType { get; }

        RocketButtonSize Size { get; }

        string Text { get; }

        string Icon { get; }

        bool? Disabled { get; }

        FontIconType IconType { get; }
    }
}