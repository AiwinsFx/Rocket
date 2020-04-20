using System;
using Aiwins.Rocket.Collections;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.DependencyInjection {
    public interface IOnServiceRegistredContext {
        ITypeList<IRocketInterceptor> Interceptors { get; }

        Type ImplementationType { get; }
    }
}