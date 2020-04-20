using System.Threading;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Uow {
    public interface ISupportsSavingChanges {
        void SaveChanges ();

        Task SaveChangesAsync (CancellationToken cancellationToken = default);
    }
}