using Microsoft.AspNetCore.Identity;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> DeleteAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Errors;
        }

        public async Task<AppUser?> GetAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<IdentityError>> AddAsync(AppUser newUser, string userPassword)
        {
            var result = await _userManager.CreateAsync(newUser, userPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }

            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdatePasswordAsync(AppUser user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Errors;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
