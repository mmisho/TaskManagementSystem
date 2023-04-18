using Domain.RoleManagement;
using Domain.TaskManagement;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
#nullable disable
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAcces
{
    public class EFDbContext : IdentityDbContext<User, Role, string>
    {
        public EFDbContext(DbContextOptions dbContext)
            : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var roleId= Guid.NewGuid().ToString();
            var userId= Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = roleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

            builder.Entity<IdentityRole>().HasData(
                new IdentityUser
                {
                    Id = userId, 
                    UserName = "myuser",
                    NormalizedUserName = "MYUSER",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password")
                });

            new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId,
            };

            base.OnModelCreating(builder);

        }

        public DbSet<Domain.TaskManagement.Task> Tasks { get; set; }
    }
}
