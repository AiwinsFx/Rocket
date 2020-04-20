using System.Threading.Tasks;

namespace Aiwins.Rocket.Features {
    public class DefaultValueFeatureValueProvider : FeatureValueProvider //TODO: 考虑直接实现IFeatureValueProvider接口
    {
        public const string ProviderName = "D";

        public override string Name => ProviderName;

        public DefaultValueFeatureValueProvider (IFeatureStore settingStore) : base (settingStore) {

        }

        public override Task<string> GetOrNullAsync (FeatureDefinition setting) {
            return Task.FromResult (setting.DefaultValue);
        }
    }
}