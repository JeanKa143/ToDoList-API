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

        public async Task CreateAsync(TaskList taskList)
        {
            Create(taskList);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskList taskList)
        {
            Delete(taskList);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskList>> GetAllByOwnerIdAndGroupIdAsync(Guid ownerId, int groupId)
        {
            return await FindByCondition(t => t.GroupId.Equals(groupId) && t.Group!.OwnerId.Equals(ownerId.ToString()))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskList?> GetByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            return await FindByCondition(t => t.Group!.OwnerId.Equals(ownerId.ToString()))
                .FirstOrDefaultAsync();
        }

        public async Task<TaskList?> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            return await FindByCondition(t => t.Group!.OwnerId.Equals(ownerId.ToString()))
                .Include(t => t.TaskItems)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TaskList taskList)
        {
            Update(taskList);
            await AppDbContext.SaveChangesAsync();
        }
    }
}
