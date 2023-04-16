#nullable disable
using Application.RoleManagement.Dtos;
using Application.Shared;

namespace Application.RoleManagement.Queries.GetRoles
{
    public class GetRolesResponse : PaginationResponse
    {
        public IEnumerable<RoleDtoModel> Roles { get; set; }
    }
}
