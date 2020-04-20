using NUglify;
using Aiwins.Rocket.Minify.Scripts;

namespace Aiwins.Rocket.Minify.NUglify {
    public class NUglifyJavascriptMinifier : NUglifyMinifierBase, IJavascriptMinifier {
        protected override UglifyResult UglifySource (string source, string fileName) {
            return Uglify.Js (source, fileName);
        }
    }
}