using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket;

namespace AiwinsDocs.Migrator
{
    public static class AppExtensions
    {
        public static T Resolve<T>(this IRocketApplicationWithInternalServiceProvider app)
        {
            return (T)app.ServiceProvider.GetRequiredService<T>();
        }
    }
}