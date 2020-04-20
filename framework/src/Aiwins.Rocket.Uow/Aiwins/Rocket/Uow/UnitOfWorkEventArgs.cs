using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Uow {
    public class UnitOfWorkEventArgs : EventArgs {
        /// <summary>
        /// 工作单元的引用实例。
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkEventArgs ([NotNull] IUnitOfWork unitOfWork) {
            Check.NotNull (unitOfWork, nameof (unitOfWork));

            UnitOfWork = unitOfWork;
        }
    }
}