using Application.UserManagement.Dtos;
using Domain.Shared;
using Domain.UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserManagement.Queries.GetUser
{

    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var response = new GetUserResponse()
            {
                User = new UserDtoModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IdNumber = user.IdNumber,
                },
            };

            return response;
        }
    }

}
