using MediatR;

namespace Application.TaskManagement.Commands.UpdateTask
{
    public record UpdateTask (Guid TaskId, string Title, string? SmallDescription, string? Description, Guid UserId) : IRequest;
}
