using Moq;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockITaskListGroupRepository
    {
        public static Mock<ITaskListGroupRepository> GetMock()
        {
            var mock = new Mock<ITaskListGroupRepository>();

            var data = Data.TaskListGroups;

            mock.Setup(m => m.GetByIdAndOwnerIdAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int id, Guid ownerId) => data.FirstOrDefault(tlg => tlg.Id == id && tlg.OwnerId == ownerId.ToString()));

            mock.Setup(m => m.GetWithDetailsByIdAndOwnerIdAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int id, Guid ownerId) => data.FirstOrDefault(tlg => tlg.Id == id && tlg.OwnerId == ownerId.ToString()));

            mock.Setup(m => m.GetAllByOwnerIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid ownerId) => data.Where(tlg => tlg.OwnerId == ownerId.ToString()).ToList());

            mock.Setup(m => m.GetAllWithDetailsByOwnerIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid ownerId) => data.Where(tlg => tlg.OwnerId == ownerId.ToString()).ToList());

            mock.Setup(m => m.IsAnyWithOwnerIdAndGroupIdAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                .ReturnsAsync((Guid ownerId, int groupId) => data.Any(tlg => tlg.Id == groupId && tlg.OwnerId == ownerId.ToString()));

            mock.Setup(m => m.Create(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Delete(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            return mock;
        }
    };
}