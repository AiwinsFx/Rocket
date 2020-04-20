using System.Threading;

namespace Aiwins.Rocket.Threading {
    public static class CancellationTokenProviderExtensions {
        public static CancellationToken FallbackToProvider (this ICancellationTokenProvider provider, CancellationToken prefferedValue = default) {
            return prefferedValue == default || prefferedValue == CancellationToken.None ?
                provider.Token :
                prefferedValue;
        }
    }
}