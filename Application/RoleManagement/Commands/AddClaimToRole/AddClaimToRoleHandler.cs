
using Domain.RoleManagement.Repositories;
using MediatR;
using System.Security.Claims;

namespace Application.RoleManagement.Commands.AddClaimToRole
{
    public class AddClaimToRoleHandler : IRequestHandler<AddClameToRole>
    {
        private readonly IRoleRepository _roleRepository;

        public AddClaimToRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Unit> Handle(AddClameToRole request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            var claim = new Claim("Task_Permission", request.ClaimType.ToString());

            await _roleRepository.AddClaimToRole(role, claim);

            return Unit.Value;
        }
    }
}
