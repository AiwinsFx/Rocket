using System;
using System.Data;

namespace Aiwins.Rocket.Uow {
    public class RocketUnitOfWorkOptions : IRocketUnitOfWorkOptions {
        /// <summary>
        /// 默认值: false
        /// </summary>
        public bool IsTransactional { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public RocketUnitOfWorkOptions () {

        }

        public RocketUnitOfWorkOptions (bool isTransactional = false, IsolationLevel? isolationLevel = null, TimeSpan? timeout = null) {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        public RocketUnitOfWorkOptions Clone () {
            return new RocketUnitOfWorkOptions {
                IsTransactional = IsTransactional,
                    IsolationLevel = IsolationLevel,
                    Timeout = Timeout
            };
        }
    }
}