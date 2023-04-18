using Application.TaskManagement.Dtos;
using Application.UserManagement.Dtos;
using Domain.TaskManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using System.Xml.Schema;

namespace Application.TaskManagement.Queries.GetTasks
{
    public class GetTasksHandler : IRequestHandler<GetTasksRequest, GetTasksResponse>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<GetTasksResponse> Handle(GetTasksRequest request, CancellationToken cancellationToken)
        {
            var tasks = _taskRepository.Query()
                                       .Include(x => x.User);

            var total = tasks.Count();

            var tasksList = await tasks.Pagination(request).ToListAsync();

            var response = new GetTasksResponse()
            {
                Tasks = tasksList?.Select(x => new TaskDtoModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    SmallDescription = x.SmallDescription,
                    Description = x.Description,
                    User = x.User == null ? default :
                    new UserDtoModel()
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        IdNumber = x.User.IdNumber,
                        UserName = x.User.UserName,
                    },
                }),
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total,
            };

            return response;
        }
    }
}
