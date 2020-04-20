using System.Threading;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.MultiTenancy {
    public class AsyncLocalCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency {
        public BasicTenantInfo Current {
            get => _currentScope.Value;
            set => _currentScope.Value = value;
        }

        private readonly AsyncLocal<BasicTenantInfo> _currentScope;

        public AsyncLocalCurrentTenantAccessor () {
            _currentScope = new AsyncLocal<BasicTenantInfo> ();
        }
    }
}