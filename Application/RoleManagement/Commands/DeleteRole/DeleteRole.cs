using MediatR;

namespace Application.RoleManagement.Commands.DeleteRole
{
    public class DeleteRole : IRequest
    {
        public Guid RoleId { get; set; }
    }
}
