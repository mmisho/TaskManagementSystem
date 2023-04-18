using Domain.Shared;
using Domain.TaskManagement.Repositories;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.TaskManagement.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTask>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskHandler(ITaskRepository taskRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateTask request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.OfIdAsync(request.TaskId);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task was not found for Id: {request.TaskId}");
            }

            var user = await _userRepository.OfIdAsync(request.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            task.Title = request.Title;
            task.SmallDescription = request.SmallDescription;
            task.Description = request.Description;
            task.UserId = user.Id;

            _taskRepository.Update(task);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
