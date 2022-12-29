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
                    OwnerId = "6934621e-7df1-44b4-bed6-f411b6e47487",
                }
            };
    }
}
