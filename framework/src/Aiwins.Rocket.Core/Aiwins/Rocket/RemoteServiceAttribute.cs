using System;
using System.Reflection;

namespace Aiwins.Rocket {
    [Serializable]
    [AttributeUsage (AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class RemoteServiceAttribute : Attribute //TODO: 考虑和IRemoteService一起移动到其他的项目中?
    {
        /// <summary>
        /// 默认值:true
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 默认值:true
        /// </summary>
        public bool IsMetadataEnabled { get; set; }

        /// <summary>
        /// 远程服务调用的名称，
        /// 可以用于区分服务端点。
        /// </summary>
        public string Name { get; set; }

        public RemoteServiceAttribute (bool isEnabled = true) {
            IsEnabled = isEnabled;
            IsMetadataEnabled = true;
        }

        public virtual bool IsEnabledFor (Type type) {
            return IsEnabled;
        }

        public virtual bool IsEnabledFor (MethodInfo method) {
            return IsEnabled;
        }

        public virtual bool IsMetadataEnabledFor (Type type) {
            return IsMetadataEnabled;
        }

        public virtual bool IsMetadataEnabledFor (MethodInfo method) {
            return IsMetadataEnabled;
        }

        public static bool IsExplicitlyEnabledFor (Type type) {
            var remoteServiceAttr = type.GetTypeInfo ().GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && remoteServiceAttr.IsEnabledFor (type);
        }

        public static bool IsExplicitlyDisabledFor (Type type) {
            var remoteServiceAttr = type.GetTypeInfo ().GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor (type);
        }

        public static bool IsMetadataExplicitlyEnabledFor (Type type) {
            var remoteServiceAttr = type.GetTypeInfo ().GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && remoteServiceAttr.IsMetadataEnabledFor (type);
        }

        public static bool IsMetadataExplicitlyDisabledFor (Type type) {
            var remoteServiceAttr = type.GetTypeInfo ().GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && !remoteServiceAttr.IsMetadataEnabledFor (type);
        }

        public static bool IsMetadataExplicitlyDisabledFor (MethodInfo method) {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && !remoteServiceAttr.IsMetadataEnabledFor (method);
        }

        public static bool IsMetadataExplicitlyEnabledFor (MethodInfo method) {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<RemoteServiceAttribute> ();
            return remoteServiceAttr != null && remoteServiceAttr.IsMetadataEnabledFor (method);
        }
    }
}