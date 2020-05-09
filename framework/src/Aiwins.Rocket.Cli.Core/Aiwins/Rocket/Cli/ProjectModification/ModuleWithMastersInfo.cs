using System.Collections.Generic;

namespace Aiwins.Rocket.Cli.ProjectModification
{
    public class ModuleWithMastersInfo : ModuleInfo
    {
        public List<ModuleWithMastersInfo> MasterModuleInfos { get; set; }
    }
}