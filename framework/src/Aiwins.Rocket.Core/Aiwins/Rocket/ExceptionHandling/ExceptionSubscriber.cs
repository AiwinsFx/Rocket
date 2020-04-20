using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.ExceptionHandling {
    [ExposeServices (typeof (IExceptionSubscriber))]
    public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency {
        public abstract Task HandleAsync (ExceptionNotificationContext context);
    }
}