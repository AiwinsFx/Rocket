using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public class DisposeAction : IDisposable {
        private readonly Action _action;

        /// <summary>
        /// 创建DisposeAction <see cref="DisposeAction"/> 对象。
        /// </summary>
        /// <param name="action">对象销毁调用的方法</param>
        public DisposeAction ([NotNull] Action action) {
            Check.NotNull (action, nameof (action));

            _action = action;
        }

        public void Dispose () {
            _action ();
        }
    }
}