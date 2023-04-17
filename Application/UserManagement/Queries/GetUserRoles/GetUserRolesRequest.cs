using MediatR;

namespace Application.UserManagement.Queries.GetUserRoles
{
    public class GetUserRolesRequest : IRequest<GetUserRolesResponse>
    {
        public Guid UserId { get; set; }
    }
}
