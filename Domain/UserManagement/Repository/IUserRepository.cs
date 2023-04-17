using Domain.RoleManagement;
using Domain.Shared;
using Domain.Shared.Repository;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IdentityResult> InsertAsync(User aggregateRoot, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<IdentityResult> AddUserToRole(User user, Role role);
        Task<(bool success, User? user)> ValidateUserAsync(string userName, string password);
    }
}
