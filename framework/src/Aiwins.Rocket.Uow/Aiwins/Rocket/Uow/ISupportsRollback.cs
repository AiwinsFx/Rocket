using System.Threading;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Uow {
    public interface ISupportsRollback {
        void Rollback ();

        Task RollbackAsync (CancellationToken cancellationToken);
    }
}