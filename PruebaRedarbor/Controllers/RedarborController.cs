using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PruebaRedarbor.Controllers
{
    /// <summary>
    /// Controller by employee manage
    /// </summary>
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RedarborController : ControllerBase
    {
        private readonly IEmployeeService _employee;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="employee"></param>
        public RedarborController(IEmployeeService employee)
        {
            _employee = employee;
        }

        /// <summary>
        /// Get all employess items
        /// </summary>
        /// <returns>Array of employee items</returns>
        /// <remarks>GET: api/Redarbor</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///             <li>NoRecords => 2</li>
        ///             <li>IsNotActive => 3</li>
        ///             <li>InvalidPassword => 4</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeListOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>GET api/Redarbor/5</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///             <li>NoRecords => 2</li>
        ///             <li>IsNotActive => 3</li>
        ///             <li>InvalidPassword => 4</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        ///     <li><b>employeeItem:</b> Informacion encontrada del empleado.</li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeIdOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>POST api/Redarbor</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///             <li>NoRecords => 2</li>
        ///             <li>IsNotActive => 3</li>
        ///             <li>InvalidPassword => 4</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeAddOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <remarks>PUT api/Redarbor/5</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///             <li>NoRecords => 2</li>
        ///             <li>IsNotActive => 3</li>
        ///             <li>InvalidPassword => 4</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeItemOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action message</returns>
        /// <remarks>DELETE api/Redarbor/5</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados.
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///             <li>NoRecords => 2</li>
        ///             <li>IsNotActive => 3</li>
        ///             <li>InvalidPassword => 4</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result.</i></li>
        /// </ul>
        /// </response>
        /// <response code="400"><strong>BadRequest</strong></response>
        /// <response code="401"><strong>UnAuthorized</strong></response>
        /// <response code="500"><strong>InternalError</strong></response>
        [ProducesResponseType(typeof(EmployeeItemOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                EmployeeItemOut output = await _employee.DeleteEmployeeAsync(id);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Problem($"Error interno al actualizar el empleado: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}