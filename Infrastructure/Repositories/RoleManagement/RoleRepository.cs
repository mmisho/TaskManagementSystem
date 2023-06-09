﻿using Domain.RoleManagement;
using Domain.RoleManagement.Repositories;
using Domain.UserManagement;
using Infrastructure.DataAcces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Repositories.RoleManagement
{
    public class RoleRepository : BaseRepository<EFDbContext, Role>, IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(EFDbContext context, RoleManager<Role> roleManager)
            : base(context)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> UpdateAsync(Role role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        public override void Update(Role aggregateRoot)
        {
            _ = this.UpdateAsync(aggregateRoot).Result;
        }

        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public override void Delete(Role aggregateRoot)
        {
            _ = this.UpdateAsync(aggregateRoot).Result;
        }
        public async Task<IdentityResult> AddClaimToRole(Role role, Claim claim)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IEnumerable<Claim>> GetClaimsByRole(Role role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }
    }
}
