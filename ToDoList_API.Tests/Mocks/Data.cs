using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal static class Data
    {
        public static List<AppUser> Users =>
            new()
            {
                new AppUser
                {
                    Id = "c0a80121-7001-4b35-9a0c-05f5ec1b26e2",
                    Email = "john_smith@mail.com",
                    UserName = "john_smith@mail.com",
                    FirstName = "John",
                    LastName = "Smith"
                },
                new AppUser
                {
                    Id = "e4293f85-be32-42e3-a338-213c4a87d886",
                    Email = "ana_mora@mail.com",
                    UserName = "ana_mora@mail.com",
                    FirstName = "Ana",
                    LastName = "Mora"
                },
                new AppUser
                {
                    Id = "6934621e-7df1-44b4-bed6-f411b6e47487",
                    Email = "juan_perez@mail.com",
                    UserName = "juan_perez@mail.com",
                    FirstName = "Juan",
                    LastName = "Perez"
                }
            };

        public static List<TaskListGroup> TaskListGroups =>
            new()
            {
                new TaskListGroup
                {
                    Id = 1,
                    IsDefault = true,
                    Name = "Default",
                    OwnerId = "6934621e-7df1-44b4-bed6-f411b6e47487",
                    TaskLists = TaskLists.Where(tl => tl.GroupId == 1).ToHashSet()
                },
                new TaskListGroup
                {
                    Id = 2,
                    IsDefault = false,
                    Name = "TLG1",
                    OwnerId = "6934621e-7df1-44b4-bed6-f411b6e47487"
                }
            };

        public static List<TaskList> TaskLists =>
            new()
            {
                new TaskList
                {
                    Id = 1,
                    IsDefault = true,
                    Name = "Default",
                    GroupId = 1,
                    TaskItems = TaskItems.Where(ti => ti.TaskListId == 1).ToHashSet()
                },
                new TaskList
                {
                    Id = 2,
                    IsDefault = false,
                    Name = "TL1",
                    GroupId = 1
                },
                new TaskList
                {
                    Id = 3,
                    IsDefault = false,
                    Name = "TL2",
                    GroupId = 1
                },
                new TaskList
                {
                    Id = 4,
                    IsDefault = false,
                    Name = "TL3",
                    GroupId = 2
                }
            };

        public static List<TaskItem> TaskItems =>
            new()
            {
                new TaskItem
                {
                    Id = 1,
                    Description = "Task 1",
                    AddedDate = DateTime.Now,
                    IsImportant = false,
                    IsInMyDay = false,
                    IsDone = false,
                    TaskListId = 1,
                    TaskSteps = TaksSteps.Where(ts => ts.TaskItemId == 1).ToHashSet()
                },
                new TaskItem
                {
                    Id = 2,
                    Description = "Task 2",
                    AddedDate = DateTime.Now,
                    IsImportant = false,
                    IsInMyDay = false,
                    IsDone = false,
                    TaskListId = 1
                },
                new TaskItem
                {
                    Id = 3,
                    Description = "Task 3",
                    AddedDate = DateTime.Now,
                    IsImportant = false,
                    IsInMyDay = false,
                    IsDone = false,
                    TaskListId = 1
                }
            };

        public static List<TaskStep> TaksSteps =>
            new()
            {
                new TaskStep
                {
                    Id = 1,
                    Description = "Step 1",
                    TaskItemId = 1
                },
                new TaskStep
                {
                    Id = 2,
                    Description = "Step 2",
                    TaskItemId = 1
                },
                new TaskStep
                {
                    Id = 3,
                    Description = "Step 3",
                    TaskItemId = 1
                }
            };
    }
}
