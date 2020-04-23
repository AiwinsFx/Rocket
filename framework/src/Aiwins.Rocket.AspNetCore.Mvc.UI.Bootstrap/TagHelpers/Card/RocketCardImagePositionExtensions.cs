namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public static class RocketCardImagePositionExtensions
    {
        public static string ToClassName(this RocketCardImagePosition position)
        {
            switch (position)
            {
                case RocketCardImagePosition.None:
                    return "card-img";
                case RocketCardImagePosition.Top:
                    return "card-img-top";
                case RocketCardImagePosition.Bottom:
                    return "card-img-bottom";
                default:
                    throw new RocketException("Unknown RocketCardImagePosition: " + position);
            }
        }
    }
}