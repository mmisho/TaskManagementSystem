using Application.Shared.Enums;
using MediatR;

namespace Application.RoleManagement.Commands.AddClaimToRole
{
    public record AddClameToRole(Guid RoleId, ClaimType ClaimType) : IRequest;
}
