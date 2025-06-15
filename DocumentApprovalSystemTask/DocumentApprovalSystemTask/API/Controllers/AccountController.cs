using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApprovalSystemTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result.Contains("Successfully"))
                return Ok(new { message = result });

            return BadRequest(new { error = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized(new { error = "Invalid credentials" });

            return Ok(new
            {
                token,
                expires = DateTime.UtcNow.AddHours(1)
            });
        }
    }
}
