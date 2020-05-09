using System.Threading.Tasks;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Analyticses
{
    public interface ICliAnalyticsCollect
    {
        Task CollectAsync(CliAnalyticsCollectInputDto input);
    }
}
