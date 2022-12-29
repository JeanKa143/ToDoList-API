using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Contracts
{
    public interface ITaskListGroupRepository : IRepository<TaskListGroup>
    {
        public Task<TaskListGroup?> GetByIdAndOwnerIdAsync(int id, Guid ownerId);
        public Task<TaskListGroup?> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId);
        public Task<IEnumerable<TaskListGroup>> GetAllByOwnerIdAsync(Guid ownerId);
        public Task<IEnumerable<TaskListGroup>> GetAllWithDetailsByOwnerIdAsync(Guid ownerId);

        public Task<bool> IsAnyWithOwnerIdAndGroupIdAsync(Guid ownerId, int groupId);
    }
}
