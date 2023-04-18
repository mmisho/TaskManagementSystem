using Domain.TaskManagement.Repositories;
using Infrastructure.DataAcces;

namespace Infrastructure.Repositories.TaskManagement
{
    public class TaskRepository : BaseRepository<EFDbContext, Domain.TaskManagement.Task>, ITaskRepository
    {
        public TaskRepository(EFDbContext eFDbContext)
            :base(eFDbContext)
        {
        }
    }
}
