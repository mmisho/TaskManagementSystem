#nullable disable
using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Domain.RoleManagement
{
    public class Role : IdentityRole, IBaseEntity<String>
    {
        public string Description { get; set; }
    }
}
