namespace Aiwins.Rocket.Data {
    public static class RocketCommonDbProperties {
        /// <summary>
        /// 数据库表名称前缀
        /// 默认值: "Rocket".
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Rocket";

        /// <summary>
        /// 默认值: null
        /// </summary>
        public static string DbSchema { get; set; } = null;
    }
}