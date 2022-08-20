using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_BAL.Models;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AccountController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> Get([FromRoute] string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDto)
        {
            var errors = await _userService.AddAsync(createUserDto);

            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(CreateUserDTO)))
                : NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> Login([FromBody] LoginDTO loginDto)
        {
            var authDto = await _authService.LoginAsync(loginDto);
            return Ok(authDto);
        }

        [HttpPost("{userId}/refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> RefreshToken([FromRoute] string userId, [FromBody] AuthDTO authDto)
        {
            if (userId != authDto.UserId)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var newAuthDto = await _authService.RefreshUserTokenAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put([FromRoute] string userId, [FromBody] UpdateUserDTO updateUserDto)
        {
            if (userId != updateUserDto.Id || GetUserIdFromToken() != updateUserDto.Id)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var errors = await _userService.UpdateAsync(updateUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserDTO)))
                : NoContent();
        }

        [HttpPut("{userId}/update-password")]
        public async Task<IActionResult> UpdatePassword([FromRoute] string userId, [FromBody] UpdatePasswordDTO updatePasswordDto)
        {
            if (userId != updatePasswordDto.UserId || GetUserIdFromToken() != updatePasswordDto.UserId)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdatePasswordDTO)))
                : NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] string userId, [FromBody] DeleteUserDTO deleteUserDto)
        {
            if (userId != deleteUserDto.Id || GetUserIdFromToken() != deleteUserDto.Id)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var errors = await _userService.DeleteAsync(deleteUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(DeleteUserDTO)))
                : NoContent();
        }

        private string GetUserIdFromToken()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");
            return idClaim?.Value ?? string.Empty;
        }
    }
}
