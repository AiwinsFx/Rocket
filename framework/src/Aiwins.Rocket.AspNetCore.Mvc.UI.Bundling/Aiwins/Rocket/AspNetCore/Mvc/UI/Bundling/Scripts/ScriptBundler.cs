using System;
using Aiwins.Rocket.AspNetCore.VirtualFileSystem;
using Aiwins.Rocket.Minify.Scripts;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.Scripts
{
    public class ScriptBundler : BundlerBase, IScriptBundler
    {
        public override string FileExtension => "js";

        public ScriptBundler(IWebContentFileProvider webContentFileProvider, IJavascriptMinifier minifier)
            : base(webContentFileProvider, minifier)
        {
        }

        protected override string ProcessBeforeAddingToTheBundle(IBundlerContext context, string filePath, string fileContent)
        {
            return fileContent.EnsureEndsWith(';') + Environment.NewLine;
        }
    }
}