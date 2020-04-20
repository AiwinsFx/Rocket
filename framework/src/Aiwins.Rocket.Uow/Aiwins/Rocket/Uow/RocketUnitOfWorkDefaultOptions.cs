using System;
using System.Data;

namespace Aiwins.Rocket.Uow {
    //TODO: 实现工作单元默认配置!

    /// <summary>
    /// 全局默认配置
    /// </summary>
    public class RocketUnitOfWorkDefaultOptions {
        /// <summary>
        /// 默认值 <see cref="UnitOfWorkTransactionBehavior.Auto"/> 。
        /// </summary>
        public UnitOfWorkTransactionBehavior TransactionBehavior { get; set; } = UnitOfWorkTransactionBehavior.Auto;

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        internal RocketUnitOfWorkOptions Normalize (RocketUnitOfWorkOptions options) {
            if (options.IsolationLevel == null) {
                options.IsolationLevel = IsolationLevel;
            }

            if (options.Timeout == null) {
                options.Timeout = Timeout;
            }

            return options;
        }

        public bool CalculateIsTransactional (bool autoValue) {
            switch (TransactionBehavior) {
                case UnitOfWorkTransactionBehavior.Enabled:
                    return true;
                case UnitOfWorkTransactionBehavior.Disabled:
                    return false;
                case UnitOfWorkTransactionBehavior.Auto:
                    return autoValue;
                default:
                    throw new RocketException ("Not implemented TransactionBehavior value: " + TransactionBehavior);
            }
        }
    }
}