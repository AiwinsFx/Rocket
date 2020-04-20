using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Timing;

namespace Aiwins.Rocket.BackgroundJobs {
    public class InMemoryBackgroundJobStore : IBackgroundJobStore, ISingletonDependency {
        private readonly ConcurrentDictionary<Guid, BackgroundJobInfo> _jobs;

        protected IClock Clock { get; }

        /// <summary>
        /// 初始化内存作业存储 <see cref="InMemoryBackgroundJobStore"/> 
        /// </summary>
        public InMemoryBackgroundJobStore (IClock clock) {
            Clock = clock;
            _jobs = new ConcurrentDictionary<Guid, BackgroundJobInfo> ();
        }

        public virtual Task<BackgroundJobInfo> FindAsync (Guid jobId) {
            return Task.FromResult (_jobs.GetOrDefault (jobId));
        }

        public virtual Task InsertAsync (BackgroundJobInfo jobInfo) {
            _jobs[jobInfo.Id] = jobInfo;

            return Task.FromResult (0);
        }

        public virtual Task<List<BackgroundJobInfo>> GetWaitingJobsAsync (int maxResultCount) {
            var waitingJobs = _jobs.Values
                .Where (t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending (t => t.Priority)
                .ThenBy (t => t.TryCount)
                .ThenBy (t => t.NextTryTime)
                .Take (maxResultCount)
                .ToList ();

            return Task.FromResult (waitingJobs);
        }

        public virtual Task DeleteAsync (Guid jobId) {
            _jobs.TryRemove (jobId, out _);

            return Task.FromResult (0);
        }

        public virtual Task UpdateAsync (BackgroundJobInfo jobInfo) {
            if (jobInfo.IsAbandoned) {
                return DeleteAsync (jobInfo.Id);
            }

            return Task.FromResult (0);
        }
    }
}