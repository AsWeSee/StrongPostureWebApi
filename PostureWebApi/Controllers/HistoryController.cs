using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostureWebApi.DBContexts;
using PostureWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostureWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryContext _context;

        public HistoryController(HistoryContext context)
        {
            _context = context;
        }

        // GET: api/<HistoryController>/test
        [HttpGet("Test")]
        public IEnumerable<string> Test()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/<HistoryController>
        [HttpGet]
        public IEnumerable<PostureState> Get()
        {
            return _context.postureHistory.ToList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<HistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id)
        {
            // ...

            try
            {
                await _context.SaveChangesAsync();
            }
            // ...
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return Conflict();
            }

            return NoContent();
        }

        // POST api/<HistoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _context.postureHistory.Add(new PostureState() {time = DateTime.Now });
            _context.SaveChanges();
        }

        // PUT api/<HistoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
