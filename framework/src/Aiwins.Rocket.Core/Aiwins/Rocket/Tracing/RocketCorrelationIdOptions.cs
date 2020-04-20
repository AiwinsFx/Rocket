namespace Aiwins.Rocket.Tracing {
    /// <summary>
    /// 应用程序调用链配置参数
    /// </summary>
    public class RocketCorrelationIdOptions {
        public string HttpHeaderName { get; set; } = "X-Correlation-Id";

        public bool SetResponseHeader { get; set; } = true;
    }
}