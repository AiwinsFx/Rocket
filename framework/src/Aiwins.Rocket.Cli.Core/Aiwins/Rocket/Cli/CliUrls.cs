namespace Aiwins.Rocket.Cli
{
    public static class CliUrls
    {
#if DEBUG
        public const string WwwRocketIo = WwwRocketIoDevelopment;

        public const string AccountRocketIo = AccountRocketIoDevelopment;

        public const string NuGetRootPath = NuGetRootPathDevelopment;
#else
        public const string WwwRocketIo = WwwRocketIoProduction;
        
        public const string AccountRocketIo = AccountRocketIoProduction;
       
        public const string NuGetRootPath = NuGetRootPathProduction;
#endif

        public const string WwwRocketIoProduction = "https://rocket.io/";
        public const string AccountRocketIoProduction = "https://account.rocket.io/";
        public const string NuGetRootPathProduction = "https://nuget.rocket.io/";

        public const string WwwRocketIoDevelopment = "https://localhost:44328/";
        public const string AccountRocketIoDevelopment = "https://localhost:44333/";
        public const string NuGetRootPathDevelopment = "https://localhost:44373/";

        public static string GetNuGetServiceIndexUrl(string apiKey)
        {
            return $"{NuGetRootPath}{apiKey}/v3/index.json";
        }

        public static string GetNuGetPackageInfoUrl(string apiKey, string packageId)
        {
            return $"{NuGetRootPath}{apiKey}/v3/package/{packageId}/index.json";
        }
    }
}
