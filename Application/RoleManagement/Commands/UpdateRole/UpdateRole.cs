using MediatR;

namespace Application.RoleManagement.Commands.UpdateRole
{
    public record UpdateRole(Guid RoleId, string Name, string Description) :IRequest;
}
