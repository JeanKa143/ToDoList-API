using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Contracts
{
    public interface ITaskListRepository : IRepository<TaskList>
    {
        public Task<TaskList?> GetByIdAndOwnerId(int id, Guid ownerId);
        public Task<TaskList?> GetWithDetailsByIdAndOwnerId(int id, Guid ownerId);
        public Task<IEnumerable<TaskList>> GetAllByOwnerIdAndGroupId(Guid ownerId, int groupId);
        public Task CreateAsync(TaskList taskList);
        public Task UpdateAsync(TaskList taskList);
        public Task DeleteAsync(TaskList taskList);
    }
}
