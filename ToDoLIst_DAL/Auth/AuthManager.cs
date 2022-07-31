using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        private AppUser? _user;

        private const string _loginProvider = "ToDoListAPI";
        private const string _refreshTokenName = "RefreshToken";

        public AuthManager(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthData?> LoginAsync(string email, string password)
        {
            _user = await _userManager.FindByEmailAsync(email);
            bool isValidPassword = await _userManager.CheckPasswordAsync(_user, password);

            if (_user is null || !isValidPassword)
            {
                return null;
            }

            return new AuthData
            {
                UserId = _user.Id,
                Token = await GenerateToken(),
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<AuthData?> RefreshUserTokenAsync(AuthData authData)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(authData.Token);
            var userId = tokenContent.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            _user = await _userManager.FindByIdAsync(userId);

            if (_user is null || _user.Id != authData.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user,
                _loginProvider, _refreshTokenName, authData.RefreshToken);

            if (isValidRefreshToken)
            {
                return new AuthData
                {
                    UserId = _user.Id,
                    Token = await GenerateToken(),
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user!, _loginProvider, _refreshTokenName);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user!, _loginProvider, _refreshTokenName);
            await _userManager.SetAuthenticationTokenAsync(_user!, _loginProvider, _refreshTokenName, newRefreshToken);

            return newRefreshToken;
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: await GetUserClaims(),
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IEnumerable<Claim>> GetUserClaims()
        {
            var roles = await _userManager.GetRolesAsync(_user!);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user!);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user!.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user!.Email),
                new Claim("userId", _user!.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            return claims;
        }
    }
}
