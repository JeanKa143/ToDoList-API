using ToDoList_BAL.Models.AppUser;

namespace ToDoList_API.Tests.ModelTests
{
    public class UserValidationTest
    {
        [Theory]
        [MemberData(nameof(CreateUesarData))]
        public void TestCreateUserModelValidation(string firstName, string lastName, string email, string password, string confirmPassword, bool isValid)
        {
            var user = new CreateUserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        [Theory]
        [MemberData(nameof(UpdateUserData))]
        public void TestUpdateUserModelValidation(Guid id, string firstName, string lastName, bool isValid)
        {
            var user = new UpdateUserDto
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        [Theory]
        [MemberData(nameof(DeleteUserData))]
        public void TestDeleteUserModelValidation(Guid id, string password, bool isValid)
        {
            var user = new DeleteUserDto
            {
                Id = id,
                Password = password
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        [Theory]
        [MemberData(nameof(UpdateUserPasswordData))]
        public void TestUpdatePasswordModelValidation(Guid id, string oldPassword, string newPassword, string confirmPassword, bool isValid)
        {
            var user = new UpdateUserPasswordDto
            {
                Id = id,
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("NotAnEmail", false)]
        [InlineData("email@mail.com", true)]
        public void TestForgotPasswordModelValidation(string email, bool isValid)
        {
            var user = new ForgotPasswordDto
            {
                Email = email
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        [Theory]
        [MemberData(nameof(ResetPasswordData))]
        public void TestResetPasswordModelValidation(string email, string password, string confirmPassword, string token, bool isValid)
        {
            var user = new ResetPasswordDto
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                Token = token
            };

            Assert.Equal(isValid, ValidateModel(user));
        }

        public static IEnumerable<object[]> CreateUesarData =>
            new List<object[]>
            {
                new object[] { null, null, null, null, null, false },
                new object[] { "TestName", "TestLastName", null, null, null, false },
                new object[] { null, null, "NotAnEmail", "Password123", "Password123", false },
                new object[] { null, null, "Test@mail.com", "Password123", "DifferentPassword123", false },
                new object[] { "TestName", "TestLastName", "NotAnEmail", "Password123", "Password123", false },
                new object[] { "TestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestNameTestName", null, "Test@mail.com", "Password123", "Password123", false },
                new object[] { null, "TestLastNameTestLastNameTestLastNameTestLastNameTestLastNameTestLastNameTestLastNameTestLastNameTestLastName", "Test@mail.com", "Password123", "Password123", false },
                new object[] { null, null, "Test@mail.com", "Password123", "Password123", true },
                new object[] { null, "TestLastName", "Test@mail.com", "Password123", "Password123", true },
                new object[] { "TestName", null, "Test@mail.com", "Password123", "Password123", true },
                new object[] { "TestName", "TestLastName", "Test@mail.com", "Password123", "Password123", true }
            };

        public static IEnumerable<object[]> UpdateUserData =>
            new List<object[]>
            {
                new object[] { Guid.NewGuid(), null, null, true },
                new object[] { Guid.NewGuid(), "UpdateName", null, true },
                new object[] { Guid.NewGuid(), null, "UpdateLastName", true },
                new object[] { Guid.NewGuid(), "UpdateName", "UpdateLastName", true }
            };

        public static IEnumerable<object[]> DeleteUserData =>
            new List<object[]>
            {
                new object[] { null, null, false },
                new object[] { Guid.NewGuid(), null, false },
                new object[] { Guid.NewGuid(), "Password123", true }
            };

        public static IEnumerable<object[]> UpdateUserPasswordData =>
            new List<object[]>
            {
                new object[] { null, null, null, null, false },
                new object[] { Guid.NewGuid(), null, null, null, false },
                new object[] { Guid.NewGuid(), "oldPassword", null, null, false },
                new object[] { Guid.NewGuid(), "oldPassword", "newPassword", null, false },
                new object[] { Guid.NewGuid(), null, "newPassword", "newPassword", false },
                new object[] { Guid.NewGuid(), "oldPassword", "newPassword", "differentPassword", false },
                new object[] { Guid.NewGuid(), "oldPassword", "newPassword", "newPassword", true }
            };

        public static IEnumerable<object[]> ResetPasswordData =>
            new List<object[]>
            {
                new object[] { null, null, null, null, false },
                new object[] { "", "", "", "", false },
                new object[] { "NotAnEmail", "Password", "Password", "Token", false },
                new object[] { "email@mail.com", "Password", "DifferentPassword", "Token", false },
                new object[] { "email@mail.com", "Password", "DifferentPassword", null, false },
                new object[] { "email@mail.com", "Password", "Password", "Token", true }
            };

        private static bool ValidateModel(object model) => Utils.ValidateModel(model);
    }
}
