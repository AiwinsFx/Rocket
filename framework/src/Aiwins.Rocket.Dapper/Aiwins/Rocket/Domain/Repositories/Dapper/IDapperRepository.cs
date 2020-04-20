using System.Data;

namespace Aiwins.Rocket.Domain.Repositories.Dapper {
    public interface IDapperRepository {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }
    }
}