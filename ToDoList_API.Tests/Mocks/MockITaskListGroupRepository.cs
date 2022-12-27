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

            var data = new List<TaskListGroup>()
            {
                new TaskListGroup
                {
                    Id = 1,
                    IsDefault = true,
                    Name = "Default",
                    OwnerId = "b171ba77-2247-4112-9aeb-fd171a433b23",
                    TaskLists = new HashSet<TaskList>
                    {
                        new TaskList
                        {
                            Id = 1,
                            IsDefault = true,
                            Name = "Default",
                            GroupId = 1,
                        },
                        new TaskList
                        {
                            Id = 2,
                            IsDefault = false,
                            Name = "TL1",
                            GroupId = 1,
                        },
                        new TaskList
                        {
                            Id = 3,
                            IsDefault = false,
                            Name = "TL2",
                            GroupId = 1,
                        }
                    }
                },
                new TaskListGroup
                {
                    Id = 2,
                    IsDefault = false,
                    Name = "TLG1",
                    OwnerId = "b171ba77-2247-4112-9aeb-fd171a433b23",
                }
            };

            mock.Setup(m => m.GetByIdAndOwnerIdAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int id, Guid ownerId) => data.FirstOrDefault(tlg => tlg.Id == id && tlg.OwnerId == ownerId.ToString()));

            mock.Setup(m => m.GetWithDetailsByIdAndOwnerIdAsync(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync((int id, Guid ownerId) => data.FirstOrDefault(tlg => tlg.Id == id && tlg.OwnerId == ownerId.ToString()));

            mock.Setup(m => m.GetAllByOwnerIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid ownerId) => data.Where(tlg => tlg.OwnerId == ownerId.ToString()).ToList());

            mock.Setup(m => m.GetAllWithDetailsByOwnerIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid ownerId) => data.Where(tlg => tlg.OwnerId == ownerId.ToString()).ToList());

            mock.Setup(m => m.CreateAsync(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateAsync(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            mock.Setup(m => m.DeleteAsync(It.IsAny<TaskListGroup>()))
                .Callback(() => { return; });

            return mock;
        }
    };
}