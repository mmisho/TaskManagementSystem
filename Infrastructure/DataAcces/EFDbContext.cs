﻿using Domain.RoleManagement;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
#nullable disable
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAcces
{
    public class EFDbContext : IdentityDbContext<User,Role, string>
    {
        public EFDbContext(DbContextOptions dbContext)
            : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
