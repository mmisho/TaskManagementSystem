using MediatR;

namespace Application.TaskManagement.Commands.DeleteTask
{
    public class DeleteTask : IRequest
    {
        public Guid TaskId { get; set; }
    }
}
