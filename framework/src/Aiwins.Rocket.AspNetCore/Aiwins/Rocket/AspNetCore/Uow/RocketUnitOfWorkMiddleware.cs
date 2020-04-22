using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Uow;
using Microsoft.AspNetCore.Http;

namespace Aiwins.Rocket.AspNetCore.Uow {
    public class RocketUnitOfWorkMiddleware : IMiddleware, ITransientDependency {
        public const string UnitOfWorkReservationName = "_RocketActionUnitOfWork";

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public RocketUnitOfWorkMiddleware (IUnitOfWorkManager unitOfWorkManager) {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task InvokeAsync (HttpContext context, RequestDelegate next) {
            using (var uow = _unitOfWorkManager.Reserve (UnitOfWorkReservationName)) {
                await next (context);
                await uow.CompleteAsync (context.RequestAborted);
            }
        }
    }
}