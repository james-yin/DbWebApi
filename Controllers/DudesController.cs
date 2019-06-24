using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DudesController : ControllerBase
    {
        private readonly InhContext _dbContext;

        public DudesController(InhContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET api/dudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dude>>> Get()
        {
            var dudes = await _dbContext.Dudes.ToArrayAsync();
            return dudes;
        }

        // GET api/dudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dude>> Get(int id)
        {
            var dude = await _dbContext.Dudes.FindAsync(id);
            if(dude == null)
            {
                return NotFound(new { message = $"No such Dude with ID of {id}." });
            }

            return Ok(dude);
        }

        // POST api/dudes
        [HttpPost]
        public async Task<ActionResult<Dude>> Post([FromBody] Dude dude)
        {
            _dbContext.Add(dude);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dude.Id }, dude);
        }

        // PUT api/dudes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dude dude)
        {
            if (id != dude.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(dude).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/dudes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todoItem = await _dbContext.Dudes.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _dbContext.Dudes.Remove(todoItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
