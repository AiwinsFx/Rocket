using System.Threading;

namespace Aiwins.Rocket.Threading
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
