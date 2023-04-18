using MediatR;

namespace Application.TaskManagement.Queries.GetTask
{
    public class GetTaskRequest : IRequest<GetTaskResponse>
    {
        public Guid TaskId { get; set; }
    }
}
