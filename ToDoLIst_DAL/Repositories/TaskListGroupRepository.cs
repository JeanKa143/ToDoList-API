using Microsoft.EntityFrameworkCore;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Data;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Repositories
{
    public class TaskListGroupRepository : Repository<TaskListGroup>, ITaskListGroupRepository
    {
        public TaskListGroupRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public async Task CreateAsync(TaskListGroup taskListGroup)
        {
            Create(taskListGroup);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskListGroup taskListGroup)
        {
            Delete(taskListGroup);
            await AppDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskListGroup>> GetAllByOwnerIdAsync(Guid ownerId)
        {
            return await FindByCondition(t => t.OwnerId.Equals(ownerId.ToString()))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskListGroup>> GetAllWithDetailsByOwnerIdAsync(Guid ownerId)
        {
            return await FindByCondition(t => t.OwnerId.Equals(ownerId.ToString()))
                .Include(t => t.TaskLists)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskListGroup?> GetByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.OwnerId.Equals(ownerId.ToString()))
                .FirstOrDefaultAsync();
        }

        public async Task<TaskListGroup?> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.OwnerId.Equals(ownerId.ToString()))
                .Include(t => t.TaskLists)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TaskListGroup taskListGroup)
        {
            Update(taskListGroup);
            await AppDbContext.SaveChangesAsync();
        }
    }
}
