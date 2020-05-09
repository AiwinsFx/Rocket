using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule)
        )]
    public class MyProjectNameDomainModule : RocketModule
    {

    }
}
