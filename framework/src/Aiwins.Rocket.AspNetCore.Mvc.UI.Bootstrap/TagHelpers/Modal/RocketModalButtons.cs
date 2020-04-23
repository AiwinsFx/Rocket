using System;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    [Flags]
    public enum RocketModalButtons
    {
        None = 0,
        Save = 1,
        Cancel = 2,
        Close = 4
    }
}