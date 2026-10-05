using Adros.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Adros.Apis.DbIntializers
{
    public class IdentityDbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Master"))
            {

                var role = new IdentityRole<Guid>
                {

                    Name = "Master",
                };

                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Teacher"))
            {

                var role = new IdentityRole<Guid>
                {

                    Name = "Teacher",
                };

                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Student"))
            {

                var role = new IdentityRole<Guid>
                {

                    Name = "Student",
                };

                await roleManager.CreateAsync(role);
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "Master@Adros.com",
                    Email = "Master@Adros.com",
                    EmailConfirmed = true,
                    FirstName = "Master",
                    LastName = "Admin"

                };



                await userManager.CreateAsync(user1, "Password123!");


                await userManager.AddToRoleAsync(user1, "Master");

            }
        }
    }
}
