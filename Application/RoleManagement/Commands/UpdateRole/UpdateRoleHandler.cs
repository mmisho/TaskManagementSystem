using Domain.RoleManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.RoleManagement.Commands.UpdateRole
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRole>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId);

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            role.Name = request.Name;
            role.Description= request.Description;
            role.NormalizedName = request.Name.ToUpper();

            await _roleRepository.InsertAsync(role);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
