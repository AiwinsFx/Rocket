namespace Aiwins.Rocket.AspNetCore.Serilog {
    public class RocketAspNetCoreSerilogOptions {
        public AllEnricherPropertyNames EnricherPropertyNames { get; } = new AllEnricherPropertyNames ();

        public class AllEnricherPropertyNames {
            /// <summary>
            /// 默认值: "TenantId".
            /// </summary>
            public string TenantId { get; set; } = "TenantId";

            /// <summary>
            /// 默认值: "UserId".
            /// </summary>
            public string UserId { get; set; } = "UserId";

            /// <summary>
            /// 默认值: "ClientId".
            /// </summary>
            public string ClientId { get; set; } = "ClientId";

            /// <summary>
            /// 默认值: "CorrelationId".
            /// </summary>
            public string CorrelationId { get; set; } = "CorrelationId";
        }
    }
}