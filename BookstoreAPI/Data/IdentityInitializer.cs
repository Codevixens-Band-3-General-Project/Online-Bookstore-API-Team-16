using Microsoft.AspNetCore.Identity;

public class IdentityInitializer
{

    public static async Task SeedRolesAndAssignToUsers(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Seed roles
       // await SeedRoles(roleManager);

        // Assign roles to users
       // await AssignRoles(userManager);
    }

    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager,string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var role = new IdentityRole
            {
                Name = roleName
            };
            await roleManager.CreateAsync(role);
        }

   
    }

    public static async Task AssignRoles(UserManager<IdentityUser> userManager, IdentityUser user,string roleName)
    {

       
            await userManager.AddToRoleAsync(user, roleName);
        
     
    
    }
}


