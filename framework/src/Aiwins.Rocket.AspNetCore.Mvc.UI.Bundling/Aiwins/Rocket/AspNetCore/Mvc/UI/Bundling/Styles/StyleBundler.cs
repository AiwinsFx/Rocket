using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Aiwins.Rocket.AspNetCore.VirtualFileSystem;
using Aiwins.Rocket.Minify.Styles;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.Styles
{
    public class StyleBundler : BundlerBase, IStyleBundler
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public override string FileExtension => "css";

        public StyleBundler(IWebContentFileProvider webContentFileProvider, ICssMinifier minifier, IWebHostEnvironment hostingEnvironment) 
            : base(webContentFileProvider, minifier)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", relativePath.RemovePreFix("/"));
        }

        protected override string ProcessBeforeAddingToTheBundle(IBundlerContext context, string filePath, string fileContent)
        {
            return CssRelativePath.Adjust(
                fileContent,
                GetAbsolutePath(filePath),
                GetAbsolutePath(context.BundleRelativePath)
            );
        }
    }
}