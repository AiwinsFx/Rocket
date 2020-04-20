using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Uow {
    /// <summary>
    /// 定义工作单元执行失败 <see cref="IUnitOfWork.Failed"/> 的事件。
    /// </summary>
    public class UnitOfWorkFailedEventArgs : UnitOfWorkEventArgs {
        /// <summary>
        /// 工作单元执行失败的异常信息，执行完成阶段 <see cref="IUnitOfWork.Complete"/> 赋值。
        /// </summary>
        [CanBeNull]
        public Exception Exception { get; }

        /// <summary>
        /// 运用指定工作单元是否手动回滚
        /// </summary>
        public bool IsRolledback { get; }

        /// <summary>
        /// 创建工作单元执行失败的事件参数 <see cref="UnitOfWorkFailedEventArgs"/> 对象。
        /// </summary>
        public UnitOfWorkFailedEventArgs ([NotNull] IUnitOfWork unitOfWork, [CanBeNull] Exception exception, bool isRolledback) : base (unitOfWork) {
            Exception = exception;
            IsRolledback = isRolledback;
        }
    }
}