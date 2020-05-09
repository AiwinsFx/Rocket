using Aiwins.Rocket.Reflection;

namespace MyCompanyName.MyProjectName.Authorization
{
    public class MyProjectNamePermissions
    {
        public const string GroupName = "MyProjectName";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MyProjectNamePermissions));
        }
    }
}