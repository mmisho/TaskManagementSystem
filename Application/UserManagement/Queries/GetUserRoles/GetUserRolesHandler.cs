using Domain.RoleManagement.Repositories;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.UserManagement.Queries.GetUserRoles
{
    public class GetUserRolesHandler : IRequestHandler<GetUserRolesRequest, GetUserRolesResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public GetUserRolesHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<GetUserRolesResponse> Handle(GetUserRolesRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var roles = await _userRepository.GetUserRolesAsync(user);
            var response = new GetUserRolesResponse()
            {
                Roles = roles,
            };

            return response;
        }
    }
}
