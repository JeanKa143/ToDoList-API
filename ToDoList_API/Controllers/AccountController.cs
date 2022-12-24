using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Route("api/user")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get([FromRoute] Guid id)
        {
            UserDto userData = await _userService.GetByIdAsync(id);
            return Ok(userData);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            IEnumerable<IdentityError> errors = await _userService.AddAsync(createUserDto);

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
            AuthDto authData = await _userService.LoginAsync(loginDto);
            return Ok(authData);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestError), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(InternalServerError))]
        [AllowAnonymous]
        [HttpPost("{id}/refresh-token")]
        public async Task<ActionResult<AuthDto>> RefreshToken([FromRoute] Guid id, [FromBody] AuthDto authDto)
        {
            if (id != authDto.UserId)
            {
                return BadRequest(new BadRequestError("Invalid user id"));
            }

            var newAuthDto = await _userService.RefreshJwtAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            IEnumerable<IdentityError> errors = await _userService.UpdateAsync(updateUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserDto)))
                : NoContent();
        }

        [HttpPut("{id}/update-password")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordDto updatePasswordDto)
        {
            IEnumerable<IdentityError> errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserPasswordDto)))
                : NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateUserIdAttribute))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromBody] DeleteUserDto deleteUserDto)
        {
            IEnumerable<IdentityError> errors = await _userService.DeleteAsync(deleteUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(DeleteUserDto)))
                : NoContent();
        }
    }
}
