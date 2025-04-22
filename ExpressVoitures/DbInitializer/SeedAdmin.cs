using Microsoft.AspNetCore.Identity;

namespace ExpressVoitures.DbInitializer
{
    public class SeedAdmin
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync("SeedAdmin"))
            {
                await roleManager.CreateAsync(new IdentityRole("SeedAdmin"));
            }

            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Azerty78!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "SeedAdmin");
                }

            }
        }
    }
}
