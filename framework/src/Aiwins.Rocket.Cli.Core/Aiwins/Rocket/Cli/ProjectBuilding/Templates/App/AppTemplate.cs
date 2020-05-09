namespace Aiwins.Rocket.Cli.ProjectBuilding.Templates.App
{
    public class AppTemplate : AppTemplateBase
    {
        /// <summary>
        /// "app".
        /// </summary>
        public const string TemplateName = "app";

        public AppTemplate() 
            : base(TemplateName)
        {
            DocumentUrl = CliConsts.DocsLink + "/en/rocket/latest/Startup-Templates/Application";
        }
    }
}