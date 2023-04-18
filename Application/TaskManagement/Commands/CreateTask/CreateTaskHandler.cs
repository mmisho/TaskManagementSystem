using Domain.Shared;
using Domain.TaskManagement.Repositories;
using Domain.UserManagement.Repository;
using MediatR;

namespace Application.TaskManagement.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTask>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskHandler(ITaskRepository taskRepository,IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CreateTask request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var task = new Domain.TaskManagement.Task()
            {
                Title = request.Title,
                SmallDescription = request.SmallDescription,
                Description = request.Description,
                UserId = user.Id,
            };

            await _taskRepository.InsertAsync(task);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
