#nullable disable
using Application.UserManagement.Dtos;

namespace Application.TaskManagement.Dtos
{
    public class TaskDtoModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SmallDescription { get; set; }
        public string Description { get; set; }
        public UserDtoModel User { get; set; }

    }
}
