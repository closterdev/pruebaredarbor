using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaRedarbor.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RedarborController : ControllerBase
    {
        private readonly IEmployeeService _employee;
        public RedarborController(IEmployeeService employee)
        {
            _employee = employee;
        }

        /// <summary>
        /// Get all employess items
        /// </summary>
        /// <returns>Array of employee items</returns>
        /// <remarks>GET: api/RedarborController</remarks>
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
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeListOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                EmployeeListOut output = await _employee.GetEmployeeAsync();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Problem($"Error interno al consultar los empleados: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get an Employee item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object employee item</returns>
        /// <remarks>GET api/RedarborController/5</remarks>
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
        ///     <li><b>employeeItem:</b> Informacion encontrada del empleado.</li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeIdOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                EmployeeIdOut output = await _employee.GetEmployeeIdAsync(id);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Problem($"Error interno al consultar el empleado: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add a new employee item
        /// </summary>
        /// <param name="input">Employee object to create</param>
        /// <returns>Action message</returns>
        /// <remarks>POST api/RedarborController</remarks>
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
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeAddOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeAddIn input)
        {
            try
            {
                EmployeeAddOut output = await _employee.CreateEmployeeAsync(input);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Problem($"Error interno al crear el empleado: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>Action message</returns>
        /// <remarks>PUT api/RedarborController/5</remarks>
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
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeItemOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EmployeeItemIn input)
        {
            try
            {
                EmployeeItemOut output = await _employee.PutEmployeeAsync(id, input);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Problem($"Error interno al actualizar el empleado: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<RedarborController>/5
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}