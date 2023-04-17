using MediatR;

namespace Application.UserManagement.Commands.AddUserToRole
{
    public record AddUserToRole (Guid UserId, Guid RoleId) : IRequest;
}
