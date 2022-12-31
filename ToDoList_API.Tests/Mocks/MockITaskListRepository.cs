using Moq;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockITaskListRepository
    {
        public static Mock<ITaskListRepository> GetMock()
        {
            var mock = new Mock<ITaskListRepository>();
            List<TaskList> data = Data.TaskLists;

            mock.Setup(m => m.GetByIdAndGroupIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int id, int groupId) => data.FirstOrDefault(tl => tl.Id == id && tl.GroupId == groupId));

            mock.Setup(m => m.GetWithDetailsByIdAndGroupIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int id, int groupId) => data.FirstOrDefault(tl => tl.Id == id && tl.GroupId == groupId));

            mock.Setup(m => m.GetAllByGroupIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int groupId) => data.Where(tl => tl.GroupId == groupId).ToList());

            mock.Setup(m => m.IsAnyWithGroupIdAndListIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int groupId, int listId) => data.Any(tl => tl.GroupId == groupId && tl.Id == listId));

            mock.Setup(m => m.Create(It.IsAny<TaskList>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<TaskList>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Delete(It.IsAny<TaskList>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
