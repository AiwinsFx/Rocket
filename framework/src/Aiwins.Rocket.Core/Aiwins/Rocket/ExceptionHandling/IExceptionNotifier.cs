using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ExceptionHandling {
    public interface IExceptionNotifier {
        Task NotifyAsync ([NotNull] ExceptionNotificationContext context);
    }
}