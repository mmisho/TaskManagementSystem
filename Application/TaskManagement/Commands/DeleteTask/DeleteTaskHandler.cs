using Domain.Shared;
using Domain.TaskManagement.Repositories;
using MediatR;

namespace Application.TaskManagement.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTask>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteTask request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.OfIdAsync(request.TaskId);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task was not found for Id: {request.TaskId}");
            }

            _taskRepository.Delete(task);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
