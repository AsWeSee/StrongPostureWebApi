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
    public class UserController : ControllerBase
    {
        private readonly HistoryContext _context;

        public UserController(HistoryContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.users.ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "id " + id.ToString();
        }

        // GET api/<UserController>/{name}
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return name;
        }

        // POST api/<UserController>
        [HttpPost("{id}")]
        public string Post(string id)
        {
            _context.users.Add(new User() { UserName = id, HeroName = id, Exp = 0, Level = 0});
            _context.SaveChanges();

            return id;
        }

        // POST api/<UserController>
        [HttpPost("value/{name}")]
        public string Post2(string name, [FromBody] string heroname)
        {
            _context.users.Add(new User() { UserName = name, HeroName = heroname, Exp = 0, Level = 0 });
            _context.SaveChanges();

            return name;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
