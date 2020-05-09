using System;
using System.Collections.Generic;
using System.Text;
using Aiwins.Rocket.Identity.MongoDB;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.MongoDB;
using Aiwins.Rocket.SettingManagement.MongoDB;
using Aiwins.Blogging.MongoDB;

namespace Aiwins.BloggingTestApp.MongoDB
{
    [DependsOn(
        typeof(RocketIdentityMongoDbModule),
        typeof(BloggingMongoDbModule),
        typeof(RocketSettingManagementMongoDbModule),
        typeof(RocketPermissionManagementMongoDbModule)
    )]
    public class BloggingTestAppMongoDbModule : RocketModule
    {
    }
}
