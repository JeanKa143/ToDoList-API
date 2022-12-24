using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoList_API.Controllers;
using ToDoList_API.Tests.Mocks;
using ToDoList_BAL.Configurations;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Models.Auth;
using ToDoList_BAL.Services;

namespace ToDoList_API.Tests
{
    public class AccountControllerTest
    {
        private readonly AccountController _accountController;

        public AccountControllerTest() => _accountController = new AccountController(GetUserService());

        [Fact]
        public async void GivenAnIdOfAnExistingUser_WhenGettingUserById_ThenUserReturns()
        {
            var userId = Guid.Parse("c0a80121-7001-4b35-9a0c-05f5ec1b26e2");

            var result = (await _accountController.Get(userId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<UserDto>(result.Value);

            var userDto = result.Value as UserDto;
            Assert.NotNull(userDto);
            Assert.Equal(userDto.Id, userId);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingUser_WhenGettingUserById_ThenNotFoundExceptionReturns()
        {
            var userId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            await Assert.ThrowsAsync<NotFoundException>(async () => await _accountController.Get(userId));
        }

        [Fact]
        public async void GivenAValidUser_WhenRegisteringUser_ThenNoContentReturns()
        {
            var createUserDto = new CreateUserDto
            {
                Email = "Test@mail.com",
                Password = "Password123",
                FirstName = "TestName",
                LastName = "TestLastName"
            };

            var result = await _accountController.Register(createUserDto) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAValidCredentials_WhenLoggingIn_ThenAuthDtoReturns()
        {
            var loginDto = new LoginDto
            {
                Email = "john_smith@mail.com",
                Password = "CorrectPassword"
            };

            var result = (await _accountController.Login(loginDto)).Result as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<AuthDto>(result.Value);

            var authDto = result.Value as AuthDto;
            Assert.NotNull(authDto);
            Assert.Equal(authDto.Id, Guid.Parse("c0a80121-7001-4b35-9a0c-05f5ec1b26e2"));
        }

        [Fact]
        public async Task GivenAnInvalidCredentials_WhenLoggingIn_ThenLoginExceptionReturns()
        {
            var loginDto = new LoginDto
            {
                Email = "john_smith@mail.com",
                Password = "InvalidPassword"
            };

            await Assert.ThrowsAsync<LoginException>(async () => await _accountController.Login(loginDto));
        }

        [Fact]
        public async void GivenAnIdOfAnExistingUser_WhenUpdatingUser_ThenNoContentReturns()
        {
            var userId = Guid.Parse("e4293f85-be32-42e3-a338-213c4a87d886");
            var updateUserDto = new UpdateUserDto
            {
                Id = userId,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };

            var result = await _accountController.Put(updateUserDto) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingUser_WhenUpdatingUser_ThenNotFoundExceptionReturns()
        {
            var userId = Guid.Parse("a6578c3b-03e5-460a-9ac5-81ee37d858e1");
            var updateUserDto = new UpdateUserDto
            {
                Id = userId,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () => await _accountController.Put(updateUserDto));
        }

        [Fact]
        public async void GivenAnIdOfAnExistingUser_WhenUpdatingPassword_ThenNoContentReturns()
        {
            var userId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var updatePasswordDto = new UpdateUserPasswordDto
            {
                Id = userId,
                OldPassword = "CorrectPassword",
                NewPassword = "NewPassword123"
            };

            var result = await _accountController.UpdatePassword(updatePasswordDto) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingUser_WhenUpdatingPassword_ThenNotFoundExceptionReturns()
        {
            var userId = Guid.Parse("e083e480-6a78-4a40-8114-dc97e864260e");
            var updatePasswordDto = new UpdateUserPasswordDto
            {
                Id = userId,
                OldPassword = "CorrectPassword",
                NewPassword = "NewPassword123"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () => await _accountController.UpdatePassword(updatePasswordDto));
        }

        [Fact]
        public async void GivenAnIdOfAnExistingUser_WhenDeletingUser_ThenNoContentReturns()
        {
            var userId = Guid.Parse("c0a80121-7001-4b35-9a0c-05f5ec1b26e2");
            var deleteUserDto = new DeleteUserDto
            {
                Id = userId,
                Password = "CorrectPassword"
            };

            var result = await _accountController.Delete(deleteUserDto) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingUser_WhenDeletingUser_ThenNotFoundExceptionReturns()
        {
            var userId = Guid.Parse("e083e480-6a78-4a40-8114-dc97e864260e");
            var deleteUserDto = new DeleteUserDto
            {
                Id = userId,
                Password = "Password"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () => await _accountController.Delete(deleteUserDto));
        }

        private static IMapper GetMapper()
        {
            var autoMapperConfiguration = new AutoMapperConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperConfiguration));

            return new Mapper(configuration);
        }

        private static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("17d929b5-b23f-4367-9d8d-9bd882a3e8cd")
                .Build();

            return configuration;
        }

        private static UserService GetUserService()
        {
            var userRepositoryMock = MockIUserRepository.GetMock();
            var userService = new UserService(userRepositoryMock.Object, GetMapper(), GetConfiguration());

            return userService;
        }
    }
}
