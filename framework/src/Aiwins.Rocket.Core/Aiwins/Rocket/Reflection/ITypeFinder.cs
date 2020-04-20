using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Reflection {
    /// <summary>
    /// 获取应用程序中的类型
    /// </summary>
    public interface ITypeFinder {
        IReadOnlyList<Type> Types { get; }
    }
}