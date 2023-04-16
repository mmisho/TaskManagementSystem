using Application.UserManagement.Dtos;
using Domain.UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = this._userRepository.Query();

            var count = users.Count();

            
            var usersList = await users.Pagination(request).ToListAsync();

            var response = new GetUsersResponse()
            {
                Users = usersList.Select(x => new UserDtoModel()
                {
                    Id = x.Id,  
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IdNumber = x.IdNumber,
                }),
            };

            return response;
        }
    }
}
