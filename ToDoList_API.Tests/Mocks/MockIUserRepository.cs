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

            var users = Data.Users;

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
