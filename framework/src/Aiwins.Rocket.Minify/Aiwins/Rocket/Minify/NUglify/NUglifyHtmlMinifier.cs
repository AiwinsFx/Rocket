using NUglify;
using Aiwins.Rocket.Minify.Html;

namespace Aiwins.Rocket.Minify.NUglify {
    public class NUglifyHtmlMinifier : NUglifyMinifierBase, IHtmlMinifier {
        protected override UglifyResult UglifySource (string source, string fileName) {
            return Uglify.Html (source, sourceFileName : fileName);
        }
    }
}