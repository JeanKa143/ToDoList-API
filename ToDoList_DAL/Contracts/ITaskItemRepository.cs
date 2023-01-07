using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_DAL.Contracts
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        public Task<TaskItem?> GetByIdAndListIdAsync(int id, int listId);
        public Task<TaskItem?> GetWithDetailsByIdAndListIdAsync(int id, int listId);
        public Task<IEnumerable<TaskItem>> GetAllByListIdAsync(int listId);

        public Task<bool> IsAnyWithListIdAndTaskItemIdAsync(int listId, int taksItemId);
    }
}
