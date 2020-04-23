namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    public static class RocketModalSizeExtensions
    {
        public static string ToClassName(this RocketModalSize size)
        {
            switch (size)
            {
                case RocketModalSize.Small:
                    return "modal-sm";
                case RocketModalSize.Large:
                    return "modal-lg";
                case RocketModalSize.ExtraLarge:
                    return "modal-xl";
                case RocketModalSize.Default:
                    return "";
                default:
                    throw new RocketException($"Unknown {nameof(RocketModalSize)}: {size}");
            }
        }
    }
}