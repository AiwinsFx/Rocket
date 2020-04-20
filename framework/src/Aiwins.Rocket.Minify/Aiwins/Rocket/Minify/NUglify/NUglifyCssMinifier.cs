using NUglify;
using Aiwins.Rocket.Minify.Styles;

namespace Aiwins.Rocket.Minify.NUglify {
    public class NUglifyCssMinifier : NUglifyMinifierBase, ICssMinifier {
        protected override UglifyResult UglifySource (string source, string fileName) {
            return Uglify.Css (source, fileName);
        }
    }
}