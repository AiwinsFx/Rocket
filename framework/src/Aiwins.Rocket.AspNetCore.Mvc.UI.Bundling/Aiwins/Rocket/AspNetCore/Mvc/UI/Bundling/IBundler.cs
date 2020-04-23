namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    public interface IBundler
    {
        string FileExtension { get; }

        BundleResult Bundle(IBundlerContext context);
    }
}