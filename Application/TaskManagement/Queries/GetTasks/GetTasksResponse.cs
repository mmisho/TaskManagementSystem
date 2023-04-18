#nullable disable
using Application.Shared;
using Application.TaskManagement.Dtos;
using Domain.TaskManagement;

namespace Application.TaskManagement.Queries.GetTasks
{
    public class GetTasksResponse : PaginationResponse
    {
        public IEnumerable<TaskDtoModel> Tasks { get; set; }
    }
}
