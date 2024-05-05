using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaRedarbor.Controllers
{
    /// <summary>
    /// Principal auth controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Generate JWT token access
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>Jwt Token Security</returns>
        /// <remarks>POST: api/AuthController/IssueToken</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        ///     <li><b>token:</b> Jwt en base64</li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(TokenOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpPost("IssueToken")]
        public async Task<IActionResult> IssueToken([FromBody] TokenIn credentials)
        {
            try
            {
                TokenOut token = await _authService.ValidateUser(credentials);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"El usuario no existe, {ex.Message}" });
            }
        }
    }
}