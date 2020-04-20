using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Emailing {
    public class BackgroundEmailSendingJob : BackgroundJob<BackgroundEmailSendingJobArgs>, ITransientDependency {
        protected IEmailSender EmailSender { get; }

        public BackgroundEmailSendingJob (IEmailSender emailSender) {
            EmailSender = emailSender;
        }

        public override void Execute (BackgroundEmailSendingJobArgs args) {
            AsyncHelper.RunSync (() => EmailSender.SendAsync (args.To, args.Subject, args.Body, args.IsBodyHtml));
        }
    }
}