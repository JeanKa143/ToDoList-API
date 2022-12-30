using ToDoList_BAL.Models.TaskList;

namespace ToDoList_API.Tests.ModelTests
{
    public class TaskListValidationTest
    {
        [Theory]
        [InlineData(null, 0, false)]
        [InlineData("TestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestName", 0, false)]
        [InlineData("TestName", 0, true)]
        public void TestCreateTaskListModelValidation(string name, int groupId, bool isValid)
        {
            var model = new CreateTaskListDto
            {
                Name = name,
                GroupId = groupId
            };

            Assert.Equal(isValid, ValidateModel(model));
        }

        [Theory]
        [InlineData(0, 0, null, false)]
        [InlineData(0, 0, "TestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestName", false)]
        [InlineData(0, 0, "TestName", true)]

        public void TestUpdateTaskListModelValidation(int id, int groupId, string name, bool isValid)
        {
            var model = new UpdateTaskListDto
            {
                Id = id,
                GroupId = groupId,
                Name = name
            };

            Assert.Equal(isValid, ValidateModel(model));

        }


        private static bool ValidateModel(object model) => Utils.ValidateModel(model);
    }
}
