using Application.RoleManagement.Dtos;
using Domain.RoleManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.RoleManagement.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = _roleRepository.Query();

            var count = roles.Count();

            var rolesList = await roles.Pagination(request).ToListAsync();

            var response = new GetRolesResponse()
            {
                Roles = rolesList.Select(x => new RoleDtoModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }),
                Page = request.Page,
                PageSize = request.PageSize,
                Total = count
            };

            return response;
        }
    }
}
