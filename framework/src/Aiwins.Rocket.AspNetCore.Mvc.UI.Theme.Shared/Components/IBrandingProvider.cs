namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components
{
    public interface IBrandingProvider
    {
        string AppName { get; }

        string LogoUrl { get; }
    }
}
