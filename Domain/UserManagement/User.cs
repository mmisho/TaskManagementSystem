#nullable disable
using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement
{
    public class User : IdentityUser, IBaseEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
    }
}
