using Application.Users;
using Application.Users.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kastel_Planner_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _userService.GetUserByIdAsync(id);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _userService.CreateUserAsync(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Details), new { id = result.Value.Id}, result.Value);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _userService.ResetPassword(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] CreatePasswordResetToken request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _userService.ForgotPassword(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest request)
        {
            var result = await _userService.Authenticate(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            SetRefreshTokenCookie(result.Value.RefreshToken, result.Value.Expires);

            return Ok(result.Value);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            string refreshToken = request.RefreshToken ?? Request.Cookies["refreshToken"];

            var result = await _userService.RefreshToken(refreshToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            SetRefreshTokenCookie(result.Value.RefreshToken, result.Value.Expires);

            return Ok(result.Value);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request)
        {
            string refreshToken = request.RefreshToken ?? Request.Cookies["refreshToken"];
            if (Request.Cookies["refreshToken"] != null)
            {
                Response.Cookies.Delete("refreshToken");
            }

            var result = await _userService.RevokeToken(refreshToken);
            
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _userService.DeleteUserAsync(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _userService.UpdateUserAsync(id, request);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        private void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = expires,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
