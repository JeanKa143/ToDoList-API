using Microsoft.EntityFrameworkCore;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Data;
using ToDoLIst_DAL.Entities;
using ToDoLIst_DAL.Repositories;

namespace ToDoList_DAL.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetAllByListIdAsync(int listId)
        {
            return await FindByCondition(t => t.TaskListId.Equals(listId))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAndListIdAsync(int id, int listId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.TaskListId.Equals(listId))
                .FirstOrDefaultAsync();
        }

        public async Task<TaskItem?> GetWithDetailsByIdAndListIdAsync(int id, int listId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.TaskListId.Equals(listId))
                .Include(t => t.TaskSteps!.OrderBy(ts => ts.Position))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsAnyWithListIdAndTaskItemIdAsync(int listId, int taksItemId)
        {
            return await FindByCondition(t => t.TaskListId.Equals(listId) && t.Id.Equals(taksItemId))
                .AnyAsync();
        }
    }
}
