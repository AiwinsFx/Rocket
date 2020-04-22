using Aiwins.Rocket.AspNetCore.VirtualFileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder {
    public static class VirtualFileSystemApplicationBuilderExtensions {
        public static IApplicationBuilder UseVirtualFiles (this IApplicationBuilder app) {
            return app.UseStaticFiles (
                new StaticFileOptions {
                    FileProvider = app.ApplicationServices.GetRequiredService<IWebContentFileProvider> ()
                }
            );
        }
    }
}