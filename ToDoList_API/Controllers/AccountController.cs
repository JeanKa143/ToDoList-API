using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList_BAL.Models;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers
{
    [Route("api/[controller]")]
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
            return HandleResponse(errors);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> Login([FromBody] LoginDTO loginDto)
        {
            var authDto = await _authService.LoginAsync(loginDto);
            return Ok(authDto);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDTO>> RefreshToken([FromBody] AuthDTO authDto)
        {
            var newAuthDto = await _authService.RefreshUserTokenAsync(authDto);
            return Ok(newAuthDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserDTO updateUserDto)
        {
            if (GetUserIdFromToken() != updateUserDto.Id)
            {
                return BadRequest();
            }

            var errors = await _userService.UpdateAsync(updateUserDto);
            return HandleResponse(errors);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO updatePasswordDto)
        {
            if (GetUserIdFromToken() != updatePasswordDto.UserId)
            {
                return BadRequest();
            }

            var errors = await _userService.UpdatePasswordAsync(updatePasswordDto);
            return HandleResponse(errors);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDTO deleteUserDto)
        {
            if (GetUserIdFromToken() != deleteUserDto.Id)
            {
                return BadRequest();
            }

            var errors = await _userService.DeleteAsync(deleteUserDto);
            return HandleResponse(errors);
        }

        private IActionResult HandleResponse(IEnumerable<IdentityError> errors)
        {
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return NoContent();
        }

        private string GetUserIdFromToken()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");
            return idClaim?.Value ?? string.Empty;
        }
    }
}
