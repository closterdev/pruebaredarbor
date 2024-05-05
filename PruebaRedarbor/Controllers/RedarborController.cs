﻿using Application.Dtos;
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
        ///     <li><b>result:</b> Indice de resultados
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result</i></li>
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

        // GET api/<RedarborController>/5
        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee item</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="input">Employee object to create</param>
        /// <returns>Employee item</returns>
        /// <remarks>POST api/RedarborController</remarks>
        /// <response code="200"><strong>Success</strong><br/>
        /// <ul>
        ///     <li><b>message:</b> Descripcion de la solicitud realizada.</li>
        ///     <li><b>result:</b> Indice de resultados
        ///         <ul>
        ///             <li>Success => 0</li>
        ///             <li>Error => 1</li>
        ///         </ul>
        ///     </li>
        ///     <li><b>resultAsString:</b> Descripcion del valor de <i>result</i></li>
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

        // PUT api/<RedarborController>/5
        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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