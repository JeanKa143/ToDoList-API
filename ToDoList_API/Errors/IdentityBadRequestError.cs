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
                case nameof(CreateUserDTO):
                    AddErrorsToDictionary(dictionary, nameof(CreateUserDTO.Email), GetUserNameAndEmailErrors(errors));
                    AddErrorsToDictionary(dictionary, nameof(CreateUserDTO.Password), GetNewPasswordErrors(errors));
                    break;
                case nameof(UpdateUserPasswordDTO):
                    AddErrorsToDictionary(dictionary, nameof(UpdateUserPasswordDTO.OldPassword), GetOldPasswordErrors(errors));
                    AddErrorsToDictionary(dictionary, nameof(UpdateUserPasswordDTO.NewPassword), GetNewPasswordErrors(errors));
                    break;
                default:
                    break;
            }

            return dictionary;
        }

        private static void AddErrorsToDictionary(Dictionary<string, IEnumerable<string>> dictionary, string key, IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                dictionary.Add(key, errors);
            }
        }

        private static IEnumerable<string> GetUserNameAndEmailErrors(IEnumerable<IdentityError> errors)
        {
            var userNameErrors = new List<string>();

            foreach (var error in errors)
            {
                if (error.Code == nameof(IdentityErrorDescriber.DuplicateUserName)
                    || error.Code == nameof(IdentityErrorDescriber.InvalidUserName)
                    || error.Code == nameof(IdentityErrorDescriber.DuplicateEmail)
                    || error.Code == nameof(IdentityErrorDescriber.InvalidEmail))
                {
                    userNameErrors.Add(error.Description);
                }
            }

            return userNameErrors;
        }

        private static IEnumerable<string> GetUserNameErrors(IEnumerable<IdentityError> errors)
        {
            var userNameErrors = new List<string>();

            foreach (var error in errors)
            {
                if (error.Code == nameof(IdentityErrorDescriber.DuplicateUserName) ||
                    error.Code == nameof(IdentityErrorDescriber.InvalidUserName))
                {
                    userNameErrors.Add(error.Description);
                }
            }

            return userNameErrors;
        }

        private static IEnumerable<string> GetEmailErrors(IEnumerable<IdentityError> errors)
        {
            var emailErrors = new List<string>();

            foreach (var error in errors)
            {
                if (error.Code == nameof(IdentityErrorDescriber.DuplicateEmail)
                    || error.Code == nameof(IdentityErrorDescriber.InvalidEmail))
                {
                    emailErrors.Add(error.Description);
                }
            }

            return emailErrors;
        }

        private static IEnumerable<string> GetNewPasswordErrors(IEnumerable<IdentityError> errors)
        {
            var passwordErrors = new List<string>();

            foreach (var error in errors)
            {
                if (error.Code == nameof(IdentityErrorDescriber.PasswordRequiresDigit)
                    || error.Code == nameof(IdentityErrorDescriber.PasswordRequiresLower)
                    || error.Code == nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric)
                    || error.Code == nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars)
                    || error.Code == nameof(IdentityErrorDescriber.PasswordRequiresUpper)
                    || error.Code == nameof(IdentityErrorDescriber.PasswordTooShort))
                {
                    passwordErrors.Add(error.Description);
                }
            }

            return passwordErrors;
        }

        private static IEnumerable<string> GetOldPasswordErrors(IEnumerable<IdentityError> errors)
        {
            var passwordErrors = new List<string>();

            foreach (var error in errors)
            {
                if (error.Code == nameof(IdentityErrorDescriber.PasswordMismatch))
                {
                    passwordErrors.Add(error.Description);
                }
            }

            return passwordErrors;
        }
    }
}
