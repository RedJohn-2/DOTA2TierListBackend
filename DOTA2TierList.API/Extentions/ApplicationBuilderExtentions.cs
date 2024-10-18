using DOTA2TierList.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DOTA2TierList.API.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationContext applicationContext = 
                scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            applicationContext.Database.Migrate();
        }
    }
}
