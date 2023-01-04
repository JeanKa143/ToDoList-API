using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_DAL.Contracts
{
    public interface ITaskStepRepository : IRepository<TaskStep>
    {
        public Task<IEnumerable<TaskStep>> GetAllByTaskItemIdAsync(int taskItemId);
        public Task<int> CountByIdsAndTaskItemIdAsync(int[] ids, int taskItemId);
    }
}
