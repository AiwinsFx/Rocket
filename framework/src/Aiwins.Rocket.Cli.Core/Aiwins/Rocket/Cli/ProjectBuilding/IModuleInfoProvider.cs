using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Cli.ProjectBuilding.Building;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public interface IModuleInfoProvider
    {
        Task<ModuleInfo> GetAsync(string name);

        Task<List<ModuleInfo>> GetModuleListAsync();
    }
}
