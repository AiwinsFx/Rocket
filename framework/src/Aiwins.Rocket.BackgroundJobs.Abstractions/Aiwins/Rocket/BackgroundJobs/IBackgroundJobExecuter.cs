using System.Threading.Tasks;

namespace Aiwins.Rocket.BackgroundJobs {
    public interface IBackgroundJobExecuter {
        Task ExecuteAsync (JobExecutionContext context);
    }
}