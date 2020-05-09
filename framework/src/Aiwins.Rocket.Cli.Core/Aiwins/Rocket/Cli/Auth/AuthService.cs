using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.IdentityModel;
using Aiwins.Rocket.IO;

namespace Aiwins.Rocket.Cli.Auth
{
    public class AuthService : ITransientDependency
    {
        protected IIdentityModelAuthenticationService AuthenticationService { get; }

        public AuthService(IIdentityModelAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        public async Task LoginAsync(string userName, string password, string organizationName = null)
        {
            var configuration = new IdentityClientConfiguration(
                CliUrls.AccountRocketIo,
                "role email rocketio rocketio_www rocketio_commercial offline_access", 
                "rocket-cli",
                "1q2w3e*",
                OidcConstants.GrantTypes.Password,
                userName,
                password
            );

            if (!organizationName.IsNullOrWhiteSpace())
            {
                configuration["[o]rocket-organization-name"] = organizationName;
            }

            var accessToken = await AuthenticationService.GetAccessTokenAsync(configuration);

            File.WriteAllText(CliPaths.AccessToken, accessToken, Encoding.UTF8);
        }

        public Task LogoutAsync()
        {
            FileHelper.DeleteIfExists(CliPaths.AccessToken);
            return Task.CompletedTask;
        }

        public static bool IsLoggedIn()
        {
            return File.Exists(CliPaths.AccessToken);
        }
    }
}
