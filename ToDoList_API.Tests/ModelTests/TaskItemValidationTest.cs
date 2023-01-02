using ToDoList_BAL.Models.TaskItem;

namespace ToDoList_API.Tests.ModelTests
{
    public class TaskItemValidationTest
    {
        [Theory]
        [MemberData(nameof(CreateTaskItemData))]
        public void TestCreateTaskItemModelValidation(int listId, string description, string note, DateTime? dueDate,
            bool isImportant, bool isInMyDay, bool isValid)
        {
            var model = new CreateTaskItemDto
            {
                TaskListId = listId,
                Description = description,
                Note = note,
                DueDate = dueDate,
                IsImportant = isImportant,
                IsInMyDay = isInMyDay
            };

            Assert.Equal(isValid, ValidateModel(model));
        }

        [Theory]
        [MemberData(nameof(UpdateTaskItemData))]
        public void TestUpdateTaskItemModelValidation(int id, int listId, string description, string note, DateTime? dueDate,
            bool isImportant, bool isInMyDay, bool isValid)
        {
            var model = new UpdateTaskItemDto
            {
                Id = id,
                TaskListId = listId,
                Description = description,
                Note = note,
                DueDate = dueDate,
                IsImportant = isImportant,
                IsInMyDay = isInMyDay
            };

            Assert.Equal(isValid, ValidateModel(model));
        }


        public static IEnumerable<object[]> CreateTaskItemData =>
            new List<object[]>
            {
                new object[] { 0, null, null, null, false, false, false },
                new object[] { 0, null, "TestNote", DateTime.Now, false, false, false },
                new object[] { 0, "TestDescription", null, null, false, false, true },
                new object[] { 0, "TestDescription", null, null, true, true, true },
                new object[] { 0, "TestDescription", "TestNote", DateTime.Now, true, true, true },
            };

        public static IEnumerable<object[]> UpdateTaskItemData =>
            new List<object[]>
            {
                new object[] { 0, 0, null, null, null, false, false, false },
                new object[] { 0, 0, null, "TestNote", DateTime.Now, false, false, false },
                new object[] { 0, 0, "TestDescription", null, null, false, false, true },
                new object[] { 0, 0, "TestDescription", null, null, true, true, true },
                new object[] { 0, 0, "TestDescription", "TestNote", DateTime.Now, true, true, true },
            };


        private static bool ValidateModel(object model) => Utils.ValidateModel(model);
    }
}
