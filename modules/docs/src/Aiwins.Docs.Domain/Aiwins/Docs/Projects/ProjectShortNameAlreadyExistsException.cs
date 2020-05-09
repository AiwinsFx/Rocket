using Aiwins.Rocket;

namespace Aiwins.Docs.Projects
{
    public class ProjectShortNameAlreadyExistsException : BusinessException
    {
        public ProjectShortNameAlreadyExistsException(string shortName)
            : base("Aiwins.Docs.Domain:010002")
        {
            WithData("ShortName", shortName);
        }
    }
}