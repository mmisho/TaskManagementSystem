#nullable disable
using Domain.Shared;
using Domain.UserManagement;

namespace Domain.TaskManagement
{
    public class Task : BaseEntity<Guid>
    {
        public override Guid Id { get; set; }

        public string Title { get; set; }
        public string? SmallDescription { get; set; }

        public string? Description { get; set; }
        public string? Attachment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
