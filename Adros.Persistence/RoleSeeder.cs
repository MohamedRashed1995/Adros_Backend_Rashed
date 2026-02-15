using Microsoft.AspNetCore.Identity;

public static class RoleSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles = { "Admin", "Teacher", "Student" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
