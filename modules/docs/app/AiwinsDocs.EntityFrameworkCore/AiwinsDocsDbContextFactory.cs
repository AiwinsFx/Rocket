using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AiwinsDocs.EntityFrameworkCore
{
    public class AiwinsDocsDbContextFactory : IDesignTimeDbContextFactory<AiwinsDocsDbContext>
    {
        public AiwinsDocsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AiwinsDocsDbContext>()
                .UseSqlServer(configuration["ConnectionString"]);

            return new AiwinsDocsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AiwinsDocs.Web/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
