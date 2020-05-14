using Aiwins.Rocket.Cli.ProjectBuilding.Templates.Module;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Templates.MvcModule
{
    public class ModuleTemplate : ModuleTemplateBase
    {
        /// <summary>
        /// "module".
        /// </summary>
        public const string TemplateName = "module";

        public ModuleTemplate()
            : base(TemplateName)
        {
            DocumentUrl = "https://docs.rocket.cn/en/rocket/latest/Startup-Templates/Module";
        }
    }
}
