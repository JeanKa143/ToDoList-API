using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models;
using ToDoLIst_DAL.Auth;

namespace ToDoList_BAL.Services
{
    public class AuthService
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;

        public AuthService(IAuthManager authManager, IMapper mapper)
        {
            _authManager = authManager;
            _mapper = mapper;
        }

        public async Task<AuthDTO> LoginAsync(LoginDTO loginDto)
        {
            var authData = await _authManager.LoginAsync(loginDto.Email, loginDto.Password);

            if (authData is null)
            {
                throw new LoginException();
            }

            return _mapper.Map<AuthDTO>(authData);
        }

        public async Task<AuthDTO> RefreshUserTokenAsync(AuthDTO authDto)
        {
            var authData = _mapper.Map<AuthData>(authDto);
            var newAuthData = await _authManager.RefreshUserTokenAsync(authData);

            if (newAuthData is null)
            {
                throw new BadRequestException("Invalid refresh token or user id");
            }

            return _mapper.Map<AuthDTO>(newAuthData);
        }
    }
}
