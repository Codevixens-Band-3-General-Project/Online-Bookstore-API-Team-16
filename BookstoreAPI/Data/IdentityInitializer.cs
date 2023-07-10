using Microsoft.AspNetCore.Identity;

public class IdentityInitializer
{

    public static async Task SeedRolesAndAssignToUsers(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Seed roles
        await SeedRoles(roleManager);

        // Assign roles to users
        await AssignRoles(userManager);
    }

    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var role = new IdentityRole
            {
                Name = "Admin"
            };
            await roleManager.CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync("NormalUser"))
        {
            var role = new IdentityRole
            {
                Name = "NormalUser"
            };
            await roleManager.CreateAsync(role);
        }
    }

    public static async Task AssignRoles(UserManager<IdentityUser> userManager)
    {
        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser != null)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var normalUser = await userManager.FindByEmailAsync("user@example.com");
        if (normalUser != null)
        {
            await userManager.AddToRoleAsync(normalUser, "NormalUser");
        }
    }
}


