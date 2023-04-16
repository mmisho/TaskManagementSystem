using Domain.Shared;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.UserManagement.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork; 
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            await _userRepository.DeleteAsync(user);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
