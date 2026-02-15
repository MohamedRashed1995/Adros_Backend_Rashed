using Adros.Core.Entities;
using Microsoft.AspNetCore.Identity;

public static class AdminSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "Master@adros.com";

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Admin",
                IsActive = true
            };

            var result = await userManager.CreateAsync(admin, "Password123!");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create Admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // التأكد من إن الـ Admin عنده role Admin
        var roles = await userManager.GetRolesAsync(admin);
        if (!roles.Contains("Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
