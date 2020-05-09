using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.IO;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Cli
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Aiwins.Rocket", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Aiwins.Rocket.Cli", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Aiwins.Rocket.Cli", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(CliPaths.Log, "rocket-cli-logs.txt"))
                .WriteTo.Console()
                .CreateLogger();

            using (var application = RocketApplicationFactory.Create<RocketCliModule>(
                options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(c => c.AddSerilog());
                }))
            {
                application.Initialize();

                AsyncHelper.RunSync(
                    () => application.ServiceProvider
                        .GetRequiredService<CliService>()
                        .RunAsync(args)
                );

                application.Shutdown();
            }
        }
    }
}
