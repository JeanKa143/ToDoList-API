using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Models.Auth;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;
using ToDoLIst_DAL.Utilities;

namespace ToDoList_BAL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IdentityError>> AddAsync(CreateUserDto createUserDto)
        {
            var newUser = _mapper.Map<AppUser>(createUserDto);
            newUser.TaskListGroups = new HashSet<TaskListGroup>
            {
                new TaskListGroup
                {
                    Name = "Default",
                    IsDefault = true,
                    TaskLists = new HashSet<TaskList>
                    {
                        new TaskList
                        {
                            Name = "Default",
                            IsDefault = true,
                        }
                    }
                }
            };

            IEnumerable<IdentityError> errors = await _userRepository.AddAsync(newUser, createUserDto.Password);
            return errors;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            AppUser user = await GetUserById(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<IdentityError>> UpdateAsync(UpdateUserDto updateUserDto)
        {
            AppUser user = await GetUserById(updateUserDto.Id);
            user = _mapper.Map(updateUserDto, user);

            IEnumerable<IdentityError> errors = await _userRepository.UpdateAsync(user);
            return errors;
        }

        public async Task<IEnumerable<IdentityError>> UpdatePasswordAsync(UpdateUserPasswordDto updatePasswordDto)
        {
            AppUser user = await GetUserById(updatePasswordDto.Id);
            IEnumerable<IdentityError> errors =
                await _userRepository.UpdatePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

            return errors;
        }

        public async Task<IEnumerable<IdentityError>> DeleteAsync(DeleteUserDto deleteUserDto)
        {
            AppUser user = await GetUserById(deleteUserDto.Id);
            bool isValidPassword = await _userRepository.CheckPasswordAsync(user, deleteUserDto.Password);

            if (!isValidPassword)
                throw new BadRequestException("Invalid password");

            IEnumerable<IdentityError> errors = await _userRepository.DeleteAsync(user);
            return errors;
        }


        public async Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            AppUser? user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user is null || !await _userRepository.CheckPasswordAsync(user, loginDto.Password))
                throw new LoginException();

            return new AuthDto
            {
                UserId = Guid.Parse(user.Id),
                Token = await CreateJwt(user),
                RefreshToken = await CreateRefreshToken(user)
            };
        }

        public async Task<AuthDto> RefreshJwtAsync(AuthDto authDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(authDto.Token);
            string? userId = tokenContent.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            AppUser? user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new BadRequestException("Invalid user id");

            bool isValidRefreshToken = await _userRepository.VerifyTokenAsync(user, authDto.RefreshToken,
                TokenProviderOptions.DefaultTokenProvider, TokenPurposeOptions.RefreshToken);

            if (!isValidRefreshToken)
                throw new BadRequestException("Invalid refresh token");

            return new AuthDto
            {
                UserId = Guid.Parse(user.Id),
                Token = await CreateJwt(user),
                RefreshToken = await CreateRefreshToken(user)
            };
        }


        private async Task<AppUser> GetUserById(Guid id)
        {
            AppUser? user = await _userRepository.GetByIdAsync(id.ToString());

            if (user is null)
                throw new NotFoundException("User", id);

            return user;
        }

        private async Task<string> CreateJwt(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: await GetUserClaims(user),
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:LifetimeInMinutes"])),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> CreateRefreshToken(AppUser user)
        {
            return await _userRepository.CreateTokenAsync(user,
                TokenProviderOptions.DefaultTokenProvider, TokenPurposeOptions.RefreshToken);
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(AppUser user)
        {
            var userClaims = await _userRepository.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id)
            }
            .Union(userClaims);

            return claims;
        }
    }
}
