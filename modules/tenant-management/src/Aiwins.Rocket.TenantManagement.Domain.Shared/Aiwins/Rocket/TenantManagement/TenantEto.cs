using System;

namespace Aiwins.Rocket.TenantManagement {
    [Serializable]
    public class TenantEto {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}