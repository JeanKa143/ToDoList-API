using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityError>> AddAsync(AppUser newUser, string userPassword);
        Task<AppUser?> GetByIdAsync(string? id);
        Task<AppUser?> GetByEmailAsync(string? email);
        Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user);
        Task<IEnumerable<IdentityError>> UpdateAsync(AppUser user);
        Task<IEnumerable<IdentityError>> UpdatePasswordAsync(AppUser user, string oldPassword, string newPassword);
        Task<IEnumerable<IdentityError>> DeleteAsync(AppUser user);

        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<bool> VerifyTokenAsync(AppUser user, string token, string provider, string purpose);
        Task<string> CreateTokenAsync(AppUser user, string provider, string purpose);
    }
}
