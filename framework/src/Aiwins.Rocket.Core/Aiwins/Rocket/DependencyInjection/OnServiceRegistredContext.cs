using System;
using Aiwins.Rocket.Collections;
using Aiwins.Rocket.DynamicProxy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.DependencyInjection {
    public class OnServiceRegistredContext : IOnServiceRegistredContext {
        public virtual ITypeList<IRocketInterceptor> Interceptors { get; }

        public virtual Type ServiceType { get; }

        public virtual Type ImplementationType { get; }

        public OnServiceRegistredContext (Type serviceType, [NotNull] Type implementationType) {
            ServiceType = Check.NotNull (serviceType, nameof (serviceType));
            ImplementationType = Check.NotNull (implementationType, nameof (implementationType));

            Interceptors = new TypeList<IRocketInterceptor> ();
        }
    }
}