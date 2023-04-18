using Domain.Shared.Repository;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain.RoleManagement.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<IdentityResult> AddClaimToRole(Role role, Claim claim);
        Task<Role> GetRoleByName(string roleName);
        Task<IEnumerable<Claim>> GetClaimsByRole(Role role);
    }
}
