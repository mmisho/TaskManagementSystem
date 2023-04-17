using MediatR;

namespace Application.IdentityManagement.Commands
{
    public record class CreateAuthenticationTokenRequest (string Username, string Password) : IRequest<CreateAuthenticationTokenResponse>;
}
