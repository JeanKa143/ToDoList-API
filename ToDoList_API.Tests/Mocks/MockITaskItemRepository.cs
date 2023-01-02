using Moq;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockITaskItemRepository
    {
        public static Mock<ITaskItemRepository> GetMock()
        {
            var mock = new Mock<ITaskItemRepository>();
            List<TaskItem> data = Data.TaskItems;

            mock.Setup(m => m.GetByIdAndListIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int id, int listId) => data.FirstOrDefault(ti => ti.Id == id && ti.TaskListId == listId));

            mock.Setup(m => m.GetWithDetailsByIdAndListIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                 .ReturnsAsync((int id, int listId) => data.FirstOrDefault(ti => ti.Id == id && ti.TaskListId == listId));

            mock.Setup(m => m.GetAllByListIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int listId) => data.Where(ti => ti.TaskListId == listId).ToList());

            mock.Setup(m => m.IsAnyWithListIdAndTaskItemIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int listId, int taskId) => data.Any(ti => ti.TaskListId == listId && ti.Id == taskId));

            mock.Setup(m => m.Create(It.IsAny<TaskItem>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<TaskItem>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Delete(It.IsAny<TaskItem>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
