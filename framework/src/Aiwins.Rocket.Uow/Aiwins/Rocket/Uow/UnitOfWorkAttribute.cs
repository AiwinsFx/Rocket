using System;
using System.Data;

namespace Aiwins.Rocket.Uow {
    /// <summary>
    /// 方法（或类的所有方法）是原子的，应应用工作单元（UOW）。
    /// </summary>
    /// <remarks>
    /// 如果在调用前已经有工作单元，则此特性无效。这种情况下，系统会使用环境UOW。
    /// </remarks>
    [AttributeUsage (AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkAttribute : Attribute {
        /// <summary>
        /// 工作单元是否以事务模式执行
        /// 默认值: null
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// 工作单元执行的超时时间
        /// 默认值: null
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 工作单元事务隔离级别.
        /// 默认值: null
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 用于指示是否启用工作单元
        /// 如果已经有一个运行中的工作单元，则该属性可忽略
        /// 默认值: false。
        /// </summary>
        public bool IsDisabled { get; set; }

        public UnitOfWorkAttribute () {

        }

        public UnitOfWorkAttribute (bool isTransactional) {
            IsTransactional = isTransactional;
        }

        public UnitOfWorkAttribute (bool isTransactional, IsolationLevel isolationLevel) {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
        }

        public UnitOfWorkAttribute (bool isTransactional, IsolationLevel isolationLevel, TimeSpan timeout) {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        //TODO: 考虑实现更多的构造方法

        public virtual void SetOptions (RocketUnitOfWorkOptions options) {
            if (IsTransactional.HasValue) {
                options.IsTransactional = IsTransactional.Value;
            }

            if (Timeout.HasValue) {
                options.Timeout = Timeout;
            }

            if (IsolationLevel.HasValue) {
                options.IsolationLevel = IsolationLevel;
            }
        }
    }
}