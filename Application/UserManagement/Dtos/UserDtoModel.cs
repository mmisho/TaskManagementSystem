#nullable disable
using Application.RoleManagement.Dtos;

namespace Application.UserManagement.Dtos
{
    public class UserDtoModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string UserName { get; set; }
    }
}
