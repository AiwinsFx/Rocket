namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    public static class RocketButtonSizeExtensions
    {
        public static string ToClassName(this RocketButtonSize size)
        {
            switch (size)
            {
                case RocketButtonSize.Small:
                    return "btn-sm";
                case RocketButtonSize.Medium:
                    return "btn-md";
                case RocketButtonSize.Large:
                    return "btn-lg";
                case RocketButtonSize.Block:
                    return "btn-block";
                case RocketButtonSize.Block_Small:
                    return "btn-sm  btn-block";
                case RocketButtonSize.Block_Medium:
                    return "btn-md  btn-block";
                case RocketButtonSize.Block_Large:
                    return "btn-lg  btn-block";
                case RocketButtonSize.Default:
                    return "";
                default:
                    throw new RocketException($"Unknown {nameof(RocketButtonSize)}: {size}");
            }
        }
    }
}