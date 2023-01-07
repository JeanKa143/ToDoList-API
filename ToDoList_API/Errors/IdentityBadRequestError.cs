using Microsoft.AspNetCore.Identity;
using ToDoList_BAL.Models.AppUser;

namespace ToDoList_API.Errors
{
    public class IdentityBadRequestError : BadRequestError
    {
        public IdentityBadRequestError(IEnumerable<IdentityError> errors, string modelClassName, string? message = DEFAULT_MESSAGE)
            : base(GetErrrors(errors, modelClassName), message)
        {
        }

        private static Dictionary<string, IEnumerable<string>> GetErrrors(IEnumerable<IdentityError> errors, string modelClassName)
        {
            var dictionary = new Dictionary<string, IEnumerable<string>>();

            switch (modelClassName)
            {
                case nameof(CreateUserDto):
                    AddErrorsToDictionary(dictionary, nameof(CreateUserDto.Email), GetUserNameAndEmailErrors(errors));
                    AddErrorsToDictionary(dictionary, nameof(CreateUserDto.Password), GetNewPasswordErrors(errors));
                    break;
                case nameof(UpdateUserPasswordDto):
                    AddErrorsToDictionary(dictionary, nameof(UpdateUserPasswordDto.OldPassword), GetOldPasswordErrors(errors));
                    AddErrorsToDictionary(dictionary, nameof(UpdateUserPasswordDto.NewPassword), GetNewPasswordErrors(errors));
                    break;
                case nameof(ResetPasswordDto):
                    AddErrorsToDictionary(dictionary, nameof(ResetPasswordDto.Password), GetNewPasswordErrors(errors));
                    break;
                default:
                    break;
            }

            return dictionary;
        }

        private static void AddErrorsToDictionary(Dictionary<string, IEnumerable<string>> dictionary, string key, IEnumerable<string> errors)
        {
            if (errors.Any())
                dictionary.Add(key, errors);
        }

        private static IEnumerable<string> GetUserNameAndEmailErrors(IEnumerable<IdentityError> errors)
        {
            var userNameErrors = new List<string>();

            foreach (IdentityError error in errors)
            {
                if (error.Code is (nameof(IdentityErrorDescriber.DuplicateUserName))
                    or (nameof(IdentityErrorDescriber.InvalidUserName))
                    or (nameof(IdentityErrorDescriber.DuplicateEmail))
                    or (nameof(IdentityErrorDescriber.InvalidEmail)))
                    userNameErrors.Add(error.Description);
            }

            return userNameErrors;
        }

        private static IEnumerable<string> GetUserNameErrors(IEnumerable<IdentityError> errors)
        {
            var userNameErrors = new List<string>();

            foreach (IdentityError error in errors)
            {
                if (error.Code is (nameof(IdentityErrorDescriber.DuplicateUserName))
                    or (nameof(IdentityErrorDescriber.InvalidUserName)))
                    userNameErrors.Add(error.Description);

            }

            return userNameErrors;
        }

        private static IEnumerable<string> GetEmailErrors(IEnumerable<IdentityError> errors)
        {
            var emailErrors = new List<string>();

            foreach (IdentityError error in errors)
            {
                if (error.Code is (nameof(IdentityErrorDescriber.DuplicateEmail))
                    or (nameof(IdentityErrorDescriber.InvalidEmail)))
                    emailErrors.Add(error.Description);

            }

            return emailErrors;
        }

        private static IEnumerable<string> GetNewPasswordErrors(IEnumerable<IdentityError> errors)
        {
            var passwordErrors = new List<string>();

            foreach (IdentityError error in errors)
            {
                if (error.Code is (nameof(IdentityErrorDescriber.PasswordRequiresDigit))
                    or (nameof(IdentityErrorDescriber.PasswordRequiresLower))
                    or (nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric))
                    or (nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars))
                    or (nameof(IdentityErrorDescriber.PasswordRequiresUpper))
                    or (nameof(IdentityErrorDescriber.PasswordTooShort)))
                    passwordErrors.Add(error.Description);

            }

            return passwordErrors;
        }

        private static IEnumerable<string> GetOldPasswordErrors(IEnumerable<IdentityError> errors)
        {
            var passwordErrors = new List<string>();

            foreach (IdentityError error in errors)
            {
                if (error.Code is (nameof(IdentityErrorDescriber.PasswordMismatch)))
                    passwordErrors.Add(error.Description);
            }

            return passwordErrors;
        }
    }
}
