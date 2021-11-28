using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostureWebApi.DBContexts;
using PostureWebApi.JsonModels;
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
        }

        // GET api/<HistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value2";
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
        [HttpPost("{id}")]
        public string Post(string id)
        {
            _context.postureHistory.Add(new PostureState() { time = DateTime.Now });
            _context.SaveChanges();

            return id;
        }

        // POST api/<HistoryController>
        [HttpPost("value/{username}")]
        public string Post2(string username, [FromBody] AddHistory addition)
        {
            User user = _context.users.SingleOrDefault(user => user.UserName == username);
            if (user == null)
            {
                return "User not exists";
            }
            _context.postureHistory.Add(new PostureState() {UserId = user.Id, quality = addition.quality, time = DateTime.Now });
            _context.SaveChanges();

            return addition.quality.ToString();
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
