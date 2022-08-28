using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_API.Filters;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Models.Auth;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(AppConventions))]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var errors = await _userService.AddAsync(createUserDto);

            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(CreateUserDto)))
                : NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedError), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto loginDto)
        {
            var authDto = await _userService.LoginAsync(loginDto);
            return Ok(authDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [AllowAnonymous]
        [HttpPost("{userId}/refresh-token")]
        public async Task<ActionResult<AuthDto>> RefreshToken([FromRoute] Guid userId, [FromBody] AuthDto authDto)
        {
            if (userId != authDto.UserId)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var newAuthDto = await _userService.RefreshJwtAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut("{userId}")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> Put([FromRoute] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var errors = await _userService.UpdateAsync(updateUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserDto)))
                : NoContent();
        }

        [HttpPut("{userId}/update-password")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid userId, [FromBody] UpdateUserPasswordDto updatePasswordDto)
        {
            var errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserPasswordDto)))
                : NoContent();
        }

        [HttpDelete("{userId}")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromBody] DeleteUserDto deleteUserDto)
        {
            var errors = await _userService.DeleteAsync(deleteUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(DeleteUserDto)))
                : Ok();
        }
    }
}
