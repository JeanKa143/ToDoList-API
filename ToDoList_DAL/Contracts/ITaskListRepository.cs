using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Contracts
{
    public interface ITaskListRepository : IRepository<TaskList>
    {
        public Task<TaskList?> GetByIdAndGroupIdAsync(int id, int groupId);
        public Task<TaskList?> GetWithDetailsByIdAndGroupIdAsync(int id, int groupId);
        public Task<IEnumerable<TaskList>> GetAllByGroupIdAsync(int groupId);

        public Task<bool> IsAnyWithGroupIdAndListIdAsync(int groupId, int listId);
    }
}
