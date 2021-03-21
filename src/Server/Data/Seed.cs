using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Data
{
    public class Seed
    {
        public static async Task SeedDatabaseAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var user = new IdentityUser
            {
                UserName = "alice@wonderland.com",
                Email = "alice@wonderland.com",
                EmailConfirmed = true,
                PhoneNumber = "000",
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");

            if (await roleManager.Roles.AnyAsync())
            {
                var role = await roleManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(user, role.Name);
            }
            else
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
