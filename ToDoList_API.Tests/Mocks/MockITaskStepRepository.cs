using Moq;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockITaskStepRepository
    {
        public static Mock<ITaskStepRepository> GetMock()
        {
            var mock = new Mock<ITaskStepRepository>();
            List<TaskStep> data = Data.TakSteps;

            mock.Setup(m => m.GetAllByTaskItemIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int taskItemId) => data.Where(ts => ts.TaskItemId == taskItemId).ToList());

            mock.Setup(m => m.CountByIdsAndTaskItemIdAsync(It.IsAny<int[]>(), It.IsAny<int>()))
                .ReturnsAsync((int[] ids, int taskItemId) => data.Count(ts => ids.Contains(ts.Id) && ts.TaskItemId == taskItemId));

            mock.Setup(m => m.Create(It.IsAny<TaskStep>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Update(It.IsAny<TaskStep>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateRange(It.IsAny<IEnumerable<TaskStep>>()))
                .Callback(() => { return; });

            mock.Setup(m => m.Delete(It.IsAny<TaskStep>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
