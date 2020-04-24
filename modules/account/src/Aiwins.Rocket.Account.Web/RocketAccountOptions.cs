namespace Aiwins.Rocket.Account.Web
{
    public class RocketAccountOptions
    {
        /// <summary>
        /// Default value: "Windows".
        /// </summary>
        public string WindowsAuthenticationSchemeName { get; set; }

        public RocketAccountOptions()
        {
            //TODO: This makes us depend on the Microsoft.AspNetCore.Server.IISIntegration package.
            WindowsAuthenticationSchemeName = "Windows"; //Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        }
    }
}
