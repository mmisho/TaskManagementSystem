using Domain.UserManagement;
using MediatR;

namespace Application.TaskManagement.Commands.CreateTask
{
    public record CreateTask(string Title, string? SmallDescription, string? Description, Guid UserId) : IRequest;
}
