using Microsoft.EntityFrameworkCore;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Data;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Repositories
{
    public class TaskListRepository : Repository<TaskList>, ITaskListRepository
    {
        public TaskListRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<TaskList>> GetAllByGroupIdAsync(int groupId)
        {
            return await FindByCondition(t => t.GroupId.Equals(groupId))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskList?> GetByIdAndGroupIdAsync(int id, int groupId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.GroupId.Equals(groupId))
                .FirstOrDefaultAsync();
        }

        public async Task<TaskList?> GetWithDetailsByIdAndGroupIdAsync(int id, int groupId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.GroupId.Equals(groupId))
                .Include(t => t.TaskItems)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
