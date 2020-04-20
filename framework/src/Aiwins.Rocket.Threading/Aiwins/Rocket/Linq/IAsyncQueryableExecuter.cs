using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Linq {
    /// <summary>
    /// 此接口供Rocket框架使用
    /// </summary>
    public interface IAsyncQueryableExecuter {
        Task<int> CountAsync<T> (IQueryable<T> queryable);

        Task<List<T>> ToListAsync<T> (IQueryable<T> queryable);

        Task<T> FirstOrDefaultAsync<T> (IQueryable<T> queryable);

        Task<T> FirstOrDefaultAsync<T> (IQueryable<T> queryable, CancellationToken cancellationToken);
    }
}