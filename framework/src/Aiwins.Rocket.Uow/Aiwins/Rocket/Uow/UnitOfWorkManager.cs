using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Uow {
    public class UnitOfWorkManager : IUnitOfWorkManager, ISingletonDependency {
        public IUnitOfWork Current => GetCurrentUnitOfWork ();

        private readonly IHybridServiceScopeFactory _serviceScopeFactory;
        private readonly IAmbientUnitOfWork _ambientUnitOfWork;

        public UnitOfWorkManager (
            IAmbientUnitOfWork ambientUnitOfWork,
            IHybridServiceScopeFactory serviceScopeFactory) {
            _ambientUnitOfWork = ambientUnitOfWork;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IUnitOfWork Begin (RocketUnitOfWorkOptions options, bool requiresNew = false) {
            Check.NotNull (options, nameof (options));

            var currentUow = Current;
            if (currentUow != null && !requiresNew) {
                return new ChildUnitOfWork (currentUow);
            }

            var unitOfWork = CreateNewUnitOfWork ();
            unitOfWork.Initialize (options);

            return unitOfWork;
        }

        public IUnitOfWork Reserve (string reservationName, bool requiresNew = false) {
            Check.NotNull (reservationName, nameof (reservationName));

            if (!requiresNew &&
                _ambientUnitOfWork.UnitOfWork != null &&
                _ambientUnitOfWork.UnitOfWork.IsReservedFor (reservationName)) {
                return new ChildUnitOfWork (_ambientUnitOfWork.UnitOfWork);
            }

            var unitOfWork = CreateNewUnitOfWork ();
            unitOfWork.Reserve (reservationName);

            return unitOfWork;
        }

        public void BeginReserved (string reservationName, RocketUnitOfWorkOptions options) {
            if (!TryBeginReserved (reservationName, options)) {
                throw new RocketException ($"Could not find a reserved unit of work with reservation name: {reservationName}");
            }
        }

        public bool TryBeginReserved (string reservationName, RocketUnitOfWorkOptions options) {
            Check.NotNull (reservationName, nameof (reservationName));

            var uow = _ambientUnitOfWork.UnitOfWork;

            //工作单元解析
            while (uow != null && !uow.IsReservedFor (reservationName)) {
                uow = uow.Outer;
            }

            if (uow == null) {
                return false;
            }

            uow.Initialize (options);

            return true;
        }

        private IUnitOfWork GetCurrentUnitOfWork () {
            var uow = _ambientUnitOfWork.UnitOfWork;

            //跳过
            while (uow != null && (uow.IsReserved || uow.IsDisposed || uow.IsCompleted)) {
                uow = uow.Outer;
            }

            return uow;
        }

        private IUnitOfWork CreateNewUnitOfWork () {
            var scope = _serviceScopeFactory.CreateScope ();
            try {
                var outerUow = _ambientUnitOfWork.UnitOfWork;

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork> ();

                unitOfWork.SetOuter (outerUow);

                _ambientUnitOfWork.SetUnitOfWork (unitOfWork);

                unitOfWork.Disposed += (sender, args) => {
                    _ambientUnitOfWork.SetUnitOfWork (outerUow);
                    scope.Dispose ();
                };

                return unitOfWork;
            } catch {
                scope.Dispose ();
                throw;
            }
        }
    }
}