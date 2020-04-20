namespace Aiwins.Rocket.MultiTenancy {
    public class RocketMultiTenancyOptions {
        /// <summary>
        /// 是否启用多租户功能
        /// 默认值: false. 
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 租户数据库的配置项
        /// 默认值: <see cref="MultiTenancyDatabaseStyle.Hybrid"/>.
        /// </summary>
        public MultiTenancyDatabaseStyle DatabaseStyle { get; set; } = MultiTenancyDatabaseStyle.Hybrid;
    }
}