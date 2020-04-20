using JetBrains.Annotations;

namespace Aiwins.Rocket.Minify {
    public interface IMinifier {
        string Minify (
            string source, [CanBeNull] string fileName = null, [CanBeNull] string originalFileName = null);
    }
}