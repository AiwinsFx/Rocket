using System;

namespace Aiwins.Rocket.Emailing {
    [Serializable]
    public class BackgroundEmailSendingJobArgs {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// 默认值: true.
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;

        //TODO: 考虑添加其他的属性字段
    }
}