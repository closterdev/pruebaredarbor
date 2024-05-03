using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaRedarbor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedarborController : ControllerBase
    {
        // GET: api/<RedarborController>
        /// <summary>
        /// Get all employess items
        /// </summary>
        /// <returns>Array of employee items</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        // POST api/<RedarborController>
        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Employee item</returns>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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