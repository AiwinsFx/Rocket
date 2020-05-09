using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameEntityFrameworkCoreTestModule)
        )]
    public class MyProjectNameDomainTestModule : RocketModule
    {

    }
}