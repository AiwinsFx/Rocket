namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components
{
    public interface IBrandingProvider
    {
        string AppName { get; }

        /// <summary>
        /// 白色背景Logo
        /// </summary>
        string LogoUrl { get; }

        /// <summary>
        /// 黑色背景Logo
        /// </summary>
        string LogoReverseUrl { get; }
    }
}
