using ToDoLIst_DAL.Contracts;

namespace ToDoList_DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskListGroupRepository TaskListGroups { get; }
        ITaskListRepository TaskLists { get; }
        Task SaveAsync();
    }
}
