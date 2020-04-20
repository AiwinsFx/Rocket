using System.Collections.Generic;
using System.Reflection;

namespace Aiwins.Rocket.Reflection {
    /// <summary>
    /// 获取应用程序中的程序集。
    /// </summary>
    public interface IAssemblyFinder {
        IReadOnlyList<Assembly> Assemblies { get; }
    }
}