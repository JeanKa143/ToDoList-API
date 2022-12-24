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
        [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
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
        [ServiceFilter(typeof(ValidateDtoIdFilter<Guid>))]
        public async Task<ActionResult<AuthDto>> RefreshToken([FromBody] AuthDto authDto)
        {
            var newAuthDto = await _userService.RefreshJwtAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
        [ServiceFilter(typeof(ValidateDtoIdFilter<Guid>))]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto updateUserDto)
        {
            IEnumerable<IdentityError> errors = await _userService.UpdateAsync(updateUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserDto)))
                : NoContent();
        }

        [HttpPut("{id}/update-password")]
        [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
        [ServiceFilter(typeof(ValidateDtoIdFilter<Guid>))]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto updatePasswordDto)
        {
            IEnumerable<IdentityError> errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(UpdateUserPasswordDto)))
                : NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
        [ServiceFilter(typeof(ValidateDtoIdFilter<Guid>))]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDto deleteUserDto)
        {
            IEnumerable<IdentityError> errors = await _userService.DeleteAsync(deleteUserDto);
            return errors.Any()
                ? BadRequest(new IdentityBadRequestError(errors, nameof(DeleteUserDto)))
                : NoContent();
        }
    }
}
