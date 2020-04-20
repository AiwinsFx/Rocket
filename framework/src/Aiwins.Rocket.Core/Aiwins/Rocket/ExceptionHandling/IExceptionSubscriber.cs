using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.ExceptionHandling {
    public interface IExceptionSubscriber {
        Task HandleAsync ([NotNull] ExceptionNotificationContext context);
    }
}