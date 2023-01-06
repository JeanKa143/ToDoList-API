using ToDoList_BAL.Models.TaskStep;

namespace ToDoList_API.Tests.ModelTests
{
    public class TaskStepValidationTest
    {
        [Theory]
        [InlineData(0, null, false, false)]
        [InlineData(0, "TestDescription", false, true)]
        [InlineData(0, "TestDescription", true, true)]
        public void TestCreateOrUpdateTaskStepModelValidation(int id, string description, bool isDone, bool isValid)
        {
            var model = new CreateOrUpdateTaskStepDto
            {
                Id = id,
                Description = description,
                IsDone = isDone
            };

            Assert.Equal(isValid, ValidateModel(model));
        }


        private static bool ValidateModel(object model) => Utils.ValidateModel(model);
    }
}
