﻿namespace Aiwins.Rocket.Data {
    public static class ConnectionStringResolverExtensions {
        public static string Resolve<T> (this IConnectionStringResolver resolver) {
            return resolver.Resolve (ConnectionStringNameAttribute.GetConnStringName<T> ());
        }
    }
}