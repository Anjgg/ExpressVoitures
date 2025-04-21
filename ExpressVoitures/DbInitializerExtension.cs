using Microsoft.AspNetCore.Identity;

namespace ExpressVoitures
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedDataBase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData.Initialize(serviceScope.ServiceProvider);
            }
            return app;
        }

        public static async Task<IApplicationBuilder> SeedAdmin(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await Admin.Initialize(serviceScope.ServiceProvider);
            }
            return app;
        }
    }
}
