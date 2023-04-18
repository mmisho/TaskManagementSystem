using Application.Shared;
using MediatR;

namespace Application.TaskManagement.Queries.GetTasks
{
    public class GetTasksRequest : PaginationRequest, IRequest<GetTasksResponse>
    {
    }
}
