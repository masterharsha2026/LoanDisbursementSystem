using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LoanDisbursementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST /api/auth/login

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);

            if(response == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                }
                );
            }

            return Ok(response);
        }
    }
}
