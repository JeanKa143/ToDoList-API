using Moq;
using System.Security.Claims;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockIUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();

            var users = new List<AppUser>()
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

            mock.Setup(m => m.AddAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Callback(() => { return; });

            mock.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => users.FirstOrDefault(u => u.Id == id));

            mock.Setup(m => m.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(u => u.Email == email));

            mock.Setup(m => m.GetClaimsAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(Enumerable.Empty<Claim>());

            mock.Setup(m => m.UpdateAsync(It.IsAny<AppUser>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdatePasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback(() => { return; });

            mock.Setup(m => m.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync((AppUser _, string password) => password.Equals("CorrectPassword"));

            mock.Setup(m => m.CreateTokenAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("TestToken");

            return mock;
        }
    }
}
