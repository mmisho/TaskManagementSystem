using Domain.RoleManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.UserManagement.Commands.AddUserToRole
{
    public class AddUserToRoleHandler : IRequestHandler<AddUserToRole>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserToRoleHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddUserToRole request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            await _userRepository.AddUserToRole(user, role);

            return Unit.Value;
        }
    }
}
