using JetBrains.Annotations;

namespace Aiwins.Rocket.TextTemplating.VirtualFiles {
    public interface ILocalizedTemplateContentReader {
        public string GetContentOrNull ([CanBeNull] string culture);
    }
}