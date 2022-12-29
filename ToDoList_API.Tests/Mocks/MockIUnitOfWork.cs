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

            return mock;
        }

    }
}
