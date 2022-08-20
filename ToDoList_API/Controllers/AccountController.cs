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
        public async Task<ActionResult<UserDTO>> Get([FromRoute] Guid userId)
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
        public async Task<ActionResult<AuthDTO>> RefreshToken([FromRoute] Guid userId, [FromBody] AuthDTO authDto)
        {
            if (userId != authDto.UserId)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var newAuthDto = await _authService.RefreshUserTokenAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put([FromRoute] Guid userId, [FromBody] UpdateUserDTO updateUserDto)
        {
            if (userId != updateUserDto.Id || GetUserIdFromToken() != updateUserDto.Id.ToString())
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var errors = await _userService.UpdateAsync(updateUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserDTO)))
                : NoContent();
        }

        [HttpPut("{userId}/update-password")]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid userId, [FromBody] UpdateUserPasswordDTO updatePasswordDto)
        {
            if (userId != updatePasswordDto.Id || GetUserIdFromToken() != updatePasswordDto.Id.ToString())
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserPasswordDTO)))
                : NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromBody] DeleteUserDTO deleteUserDto)
        {
            if (userId != deleteUserDto.Id || GetUserIdFromToken() != deleteUserDto.Id.ToString())
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
