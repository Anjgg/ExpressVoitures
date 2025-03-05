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
    }
}
