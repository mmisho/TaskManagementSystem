#nullable disable
using Domain.RoleManagement;
using Domain.UserManagement;
using Domain.UserManagement.Repository;
using Infrastructure.DataAcces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.UserManagement
{
    public class UserRepository : BaseRepository<EFDbContext, User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(EFDbContext context, UserManager<User> userManager)
            : base(context)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }
        public override void Delete(User aggregateRoot)
        {
            _ = this.DeleteAsync(aggregateRoot).Result;
        }
        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await this._userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> InsertAsync(User aggregateRoot, string password)
        {
            var result = await _userManager.CreateAsync(aggregateRoot, password);

            return result;
        }
        public override Task InsertAsync(User aggregateRoot)
        {
            throw new NotSupportedException("Not supported method.");
        }


        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> AddUserToRole(User user, Role role)
        {
           return await _userManager.AddToRoleAsync(user, role.Name);
        }
     

        public async Task<(bool success, User? user)> ValidateUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                return (result, user);
            }

            return (false, null);
        }
    }
}
