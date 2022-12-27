using ToDoList_BAL.Models.TaskListGroup;

namespace ToDoList_API.Tests.ModelTests
{
    public class TaskListGroupValidationTest
    {
        [Theory]
        [InlineData(null, "00000000-0000-0000-0000-000000000000", false)]
        [InlineData("TestName", "00000000-0000-0000-0000-000000000000", true)]
        public void TestCreateTaskListGroupModelValidation(string name, Guid ownerId, bool isValid)
        {
            var taskListGroup = new CreateTaskListGroupDto
            {
                Name = name,
                OwnerId = ownerId
            };

            Assert.Equal(isValid, ValidateModel(taskListGroup));
        }

        [Theory]
        [InlineData(0, "00000000-0000-0000-0000-000000000000", null, false)]
        [InlineData(0, "00000000-0000-0000-0000-000000000000", "Test Name", true)]
        public void TestUpdateTaskListGroupModelValidation(int id, Guid ownerId, string name, bool isValid)
        {
            var taskListGroup = new UpdateTaskListGroupDto
            {
                Id = id,
                Name = name,
                OwnerId = ownerId
            };

            Assert.Equal(isValid, ValidateModel(taskListGroup));
        }


        private static bool ValidateModel(object model) => Utils.ValidateModel(model);
    }
}
