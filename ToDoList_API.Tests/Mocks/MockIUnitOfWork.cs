using Moq;
using ToDoList_DAL.Contracts;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockIUnitOfWork
    {
        public static Mock<IUnitOfWork> GetMock()
        {
            var mock = new Mock<IUnitOfWork>();

            mock.Setup(m => m.TaskListGroups)
                .Returns(MockITaskListGroupRepository.GetMock().Object);

            mock.Setup(m => m.TaskLists)
                .Returns(MockITaskListRepository.GetMock().Object);

            mock.Setup(m => m.SaveAsync())
                .Callback(() => { return; });

            mock.Setup(m => m.Dispose())
                .Callback(() => { return; });

            return mock;
        }

    }
}
