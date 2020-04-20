using System;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Auditing {
    public interface IAuditLogSaveHandle : IDisposable {
        Task SaveAsync ();
    }
}