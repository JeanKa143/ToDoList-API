using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.Errors;
        }

        public async Task<AppUser?> GetByIdAsync(string? id)
        {
            if (id is null)
                return null;

            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<IdentityError>> AddAsync(AppUser newUser, string userPassword)
        {
            IdentityResult result = await _userManager.CreateAsync(newUser, userPassword);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(newUser, "User");

            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdateAsync(AppUser user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdatePasswordAsync(AppUser user, string oldPassword, string newPassword)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Errors;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<AppUser?> GetByEmailAsync(string? email)
        {
            if (email is null)
                return null;

            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

            return userClaims.Union(roleClaims);
        }

        public async Task<bool> VerifyTokenAsync(AppUser user, string token, string provider, string purpose)
        {
            bool isValidToken = await _userManager.VerifyUserTokenAsync(user, provider, purpose, token);

            if (!isValidToken)
                await _userManager.UpdateSecurityStampAsync(user);

            return isValidToken;
        }

        public async Task<string> CreateTokenAsync(AppUser user, string provider, string purpose)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, provider, purpose);
            var newToken = await _userManager.GenerateUserTokenAsync(user, provider, purpose);
            await _userManager.SetAuthenticationTokenAsync(user, provider, purpose, newToken);

            return newToken;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
