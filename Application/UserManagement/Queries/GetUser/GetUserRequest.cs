using MediatR;

namespace Application.UserManagement.Queries.GetUser
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
