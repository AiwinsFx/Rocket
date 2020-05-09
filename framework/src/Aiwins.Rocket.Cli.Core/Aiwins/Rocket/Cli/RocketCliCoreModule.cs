using System;
using System.Text;
using Aiwins.Rocket.Cli.Commands;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.IdentityModel;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Cli
{
    [DependsOn(
        typeof(RocketDddDomainModule),
        typeof(RocketJsonModule),
        typeof(RocketIdentityModelModule)
    )]
    public class RocketCliCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Configure<RocketCliOptions>(options =>
            {
                options.Commands["help"] = typeof(HelpCommand);
                options.Commands["new"] = typeof(NewCommand);
                options.Commands["get-source"] = typeof(GetSourceCommand);
                options.Commands["update"] = typeof(UpdateCommand);
                options.Commands["add-package"] = typeof(AddPackageCommand);
                options.Commands["add-module"] = typeof(AddModuleCommand);
                options.Commands["login"] = typeof(LoginCommand);
                options.Commands["logout"] = typeof(LogoutCommand); 
                options.Commands["generate-proxy"] = typeof(GenerateProxyCommand); 
                options.Commands["suite"] = typeof(SuiteCommand);
                options.Commands["switch-to-preview"] = typeof(SwitchNightlyPreviewCommand);
                options.Commands["switch-to-stable"] = typeof(SwitchStableCommand); 
            });
        }
    }
}
