#nullable disable
using Application.TaskManagement.Dtos;

namespace Application.TaskManagement.Queries.GetTask
{
    public class GetTaskResponse 
    {
        public TaskDtoModel Task { get; set; }
    }
}
