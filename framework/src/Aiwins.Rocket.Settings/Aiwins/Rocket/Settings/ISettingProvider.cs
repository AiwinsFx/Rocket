using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Settings {
    public interface ISettingProvider {
        Task<string> GetOrNullAsync ([NotNull] string name);

        Task<List<SettingValue>> GetAllAsync ();
    }
}