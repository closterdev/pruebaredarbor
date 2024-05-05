using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaRedarbor.Controllers
{
    /// <summary>
    /// Principal controller security
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IAuthService _authService;

        public SecurityController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Generate JWT token access
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Generate([FromBody] UserCredentials credentials)
        {
            try
            {
                var token = _authService.JwtToken(credentials.Username, credentials.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"El usuario no existe, {ex.Message}" });
            }
        }
    }
}