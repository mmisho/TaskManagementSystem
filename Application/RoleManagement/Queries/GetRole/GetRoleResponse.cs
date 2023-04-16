#nullable disable
using Application.RoleManagement.Dtos;
using Application.Shared;
using Shared.Interfaces;

namespace Application.RoleManagement.Queries.GetRole
{
    public class GetRoleResponse 
    {
        public RoleDtoModel Role { get; set; }
    }
}
