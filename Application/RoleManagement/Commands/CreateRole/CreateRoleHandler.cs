using Domain.RoleManagement;
using Domain.RoleManagement.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.RoleManagement.Commands.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRole>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var role = new Role()
            {
                Name = request.Name,
                NormalizedName = request.Name.ToUpper(),
                Description = request.Description,
            };

            await _roleRepository.InsertAsync(role);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
