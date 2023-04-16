using Domain.RoleManagement.Repositories;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RoleManagement.Commands.DeleteRole
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRole>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId);

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            _roleRepository.Delete(role);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
