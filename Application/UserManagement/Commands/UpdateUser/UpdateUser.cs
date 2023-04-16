using MediatR;

namespace Application.UserManagement.Commands.UpdateUser
{
    public record UpdateUser(Guid UserId, string FirstName, string LastName,
                              string Email, string IdNumber) : IRequest;
}
