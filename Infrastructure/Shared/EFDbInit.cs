using Domain.RoleManagement;
using Domain.UserManagement;
using Infrastructure.DataAcces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Infrastructure.Shared
{
    public class EFDbInit
    {
        public static void Initialize(EFDbContext context, IServiceScope serviceScope)
        {
            _ = new EFDbInit();
            Seed( context, serviceScope);
        }

        protected async static void Seed(EFDbContext context, IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();

            if (roleManager != null & userManager != null)
            {
                var adminRoleName = "Administrator";

                var existingRole = await roleManager!.FindByNameAsync("Administrator");

                if (existingRole == null)
                {

                    var administratorRole = new Role() { Name = "Administrator", Description = "Admin of the company"};
                    await roleManager.CreateAsync(administratorRole);
                }

                var userExists = userManager!.GetUsersInRoleAsync(adminRoleName).Result;

                if (userExists.Count() == 0)
                {
                    var user = new User
                    {
                        Email = "TaskAdmin@gmail.com",
                        UserName = "TaskAdmin@gmail.com",
                    };

                    _ = userManager.CreateAsync(user, "Password").Result;
                    _ = userManager.AddToRoleAsync(user, "Administrator").Result;
                }
            }
        }
    }
}

