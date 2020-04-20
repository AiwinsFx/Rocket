using System;
using Aiwins.Rocket.Data;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    [Serializable]
    public class TenantConfiguration {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }

        public TenantConfiguration () {

        }

        public TenantConfiguration (Guid id, [NotNull] string name) {
            Check.NotNull (name, nameof (name));

            Id = id;
            Name = name;

            ConnectionStrings = new ConnectionStrings ();
        }
    }
}