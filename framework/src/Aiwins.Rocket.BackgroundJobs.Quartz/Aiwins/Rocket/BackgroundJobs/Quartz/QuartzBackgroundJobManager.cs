﻿using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Quartz;

namespace Aiwins.Rocket.BackgroundJobs.Quartz {
    [Dependency (ReplaceServices = true)]
    public class QuartzBackgroundJobManager : IBackgroundJobManager, ITransientDependency {
        private readonly IScheduler _scheduler;

        public QuartzBackgroundJobManager (IScheduler scheduler) {
            _scheduler = scheduler;
        }

        public async Task<string> EnqueueAsync<TArgs> (TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) {
            var jobDetail = JobBuilder.Create<QuartzJobExecutionAdapter<TArgs>> ().SetJobData (new JobDataMap { { nameof (TArgs), args } }).Build ();
            var trigger = !delay.HasValue ? TriggerBuilder.Create ().StartNow ().Build () : TriggerBuilder.Create ().StartAt (new DateTimeOffset (DateTimeOffset.Now.Add (delay.Value))).Build ();
            await _scheduler.ScheduleJob (jobDetail, trigger);
            return jobDetail.Key.ToString ();
        }
    }
}