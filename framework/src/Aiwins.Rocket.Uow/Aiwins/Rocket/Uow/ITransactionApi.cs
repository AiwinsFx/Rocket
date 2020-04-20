using System;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Uow {
    public interface ITransactionApi : IDisposable {
        void Commit ();

        Task CommitAsync ();
    }
}