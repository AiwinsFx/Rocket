using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameApplicationModule),
        typeof(MyProjectNameDomainTestModule)
        )]
    public class MyProjectNameApplicationTestModule : RocketModule
    {

    }
}