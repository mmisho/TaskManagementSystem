using Domain.RoleManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement;
using Domain.UserManagement.Repository;
using Infrastructure.DataAcces;
using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {

            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                IdNumber = request.IdNumber,
                UserName = request.Email,
            };

            var result = await _userRepository.InsertAsync(user, request.Password, role);
            if (result.Succeeded)
            {
                await _unitOfWork.SaveAsync();
            }


            return Unit.Value;
        }
    }
}
