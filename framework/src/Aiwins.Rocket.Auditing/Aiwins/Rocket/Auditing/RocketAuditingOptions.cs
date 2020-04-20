using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Aiwins.Rocket.Auditing {
    public class RocketAuditingOptions {
        //TODO: 考虑添加一个选项以禁用应用程序服务方法的审计功能?

        /// <summary>
        /// 如果值为true，则审计将不会引发异常，并在保存审计日志出错时记录异常。
        /// 默认值: true.
        /// </summary>
        public bool HideErrors { get; set; }

        /// <summary>
        /// 默认值: true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 应用程序或者服务名称.
        /// 默认值: null.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 默认值: true.
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 是否记录异常日志.
        /// 默认值: true.
        /// </summary>
        public bool AlwaysLogOnException { get; set; }

        public List<AuditLogContributor> Contributors { get; }

        public List<Type> IgnoredTypes { get; }

        public IEntityHistorySelectorList EntityHistorySelectors { get; }

        //TODO: 考虑将这个属性移动到asp net core 项目，或者使用一个更加灵活的策略?
        /// <summary>
        /// 默认值: false.
        /// </summary>
        public bool IsEnabledForGetRequests { get; set; }

        public RocketAuditingOptions () {
            IsEnabled = true;
            IsEnabledForAnonymousUsers = true;
            HideErrors = true;
            AlwaysLogOnException = true;

            Contributors = new List<AuditLogContributor> ();

            IgnoredTypes = new List<Type> {
                typeof (Stream),
                typeof (Expression)
            };

            EntityHistorySelectors = new EntityHistorySelectorList ();
        }
    }
}