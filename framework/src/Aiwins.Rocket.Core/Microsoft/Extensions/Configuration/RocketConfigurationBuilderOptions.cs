using System.Reflection;

namespace Microsoft.Extensions.Configuration {
    public class RocketConfigurationBuilderOptions {
        /// <summary>
        /// 设置应用程序机密信息程序集
        /// 或者使用 <see cref="UserSecretsId"/> (具有更高的优先级)。
        /// </summary>
        public Assembly UserSecretsAssembly { get; set; }

        /// <summary>
        /// 设置应用程序机密信息标识(推荐)。
        /// 或者使用 <see cref="UserSecretsAssembly"/>
        /// </summary>
        public string UserSecretsId { get; set; }

        /// <summary>
        /// 设置应用程序用户配置文件的名称 默认值:appsettings。
        /// </summary>
        public string FileName { get; set; } = "appsettings";

        /// <summary>
        /// 设置应用程序运行环境. 可选值:"Development"、"Staging"、"Production"。
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// 设置应用程序的根路径，获取配置文件 <see cref="FileName"/>。
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// 设置环境变量前缀。
        /// </summary>
        public string EnvironmentVariablesPrefix { get; set; }

        /// <summary>
        /// 设置命令行变量。
        /// </summary>
        public string[] CommandLineArgs { get; set; }
    }
}