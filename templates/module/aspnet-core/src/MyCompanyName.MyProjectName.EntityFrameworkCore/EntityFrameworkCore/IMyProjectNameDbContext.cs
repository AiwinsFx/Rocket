using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
    public interface IMyProjectNameDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}