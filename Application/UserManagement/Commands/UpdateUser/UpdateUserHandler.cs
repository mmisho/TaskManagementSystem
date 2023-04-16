using Domain.Shared;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.UserManagement.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }
            
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdNumber = request.IdNumber;

            await _userRepository.InsertAsync(user);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
