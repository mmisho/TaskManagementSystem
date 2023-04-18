using Application.TaskManagement.Dtos;
using Application.UserManagement.Dtos;
using Domain.TaskManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TaskManagement.Queries.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskRequest, GetTaskResponse>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<GetTaskResponse> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.Query()
                                      .Where(x => x.Id == request.TaskId)
                                      .Include(x => x.User)
                                      .FirstOrDefaultAsync();

            if (task == null)
            {
                throw new KeyNotFoundException($"Task was not found for Id: {request.TaskId}");
            }

            var response = new GetTaskResponse()
            {
                Task = new TaskDtoModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    SmallDescription = task.SmallDescription,
                    Description = task.Description,
                    User = task.User == null ? default :
                    new UserDtoModel()
                    {
                        Id = task.User.Id,
                        FirstName = task.User.FirstName,
                        LastName = task.User.LastName,
                        IdNumber = task.User.IdNumber,
                        UserName = task.User.UserName,
                    }
                }
            };

            return response;
        }
    }
}
