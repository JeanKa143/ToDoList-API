using Microsoft.EntityFrameworkCore;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Data;
using ToDoLIst_DAL.Entities;
using ToDoLIst_DAL.Repositories;

namespace ToDoList_DAL.Repositories
{
    public class TaskStepRepository : Repository<TaskStep>, ITaskStepRepository
    {
        public TaskStepRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public async Task<int> CountByIdsAndTaskItemIdAsync(int[] ids, int taskItemId)
        {
            return await FindByCondition(t => t.TaskItemId.Equals(taskItemId) && ids.Contains(t.Id))
                .CountAsync();
        }

        public async Task<IEnumerable<TaskStep>> GetAllByTaskItemIdAsync(int taskItemId)
        {
            return await FindByCondition(t => t.TaskItemId.Equals(taskItemId))
                .OrderBy(t => t.Position)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
