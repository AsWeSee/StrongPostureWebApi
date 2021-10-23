using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostureWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostureWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly DatabaseContext dbContext = new DatabaseContext();
        // GET: api/<HistoryController>
        [HttpGet]
        public IEnumerable<PostureState> Get()
        {
            return dbContext.postureHistory.ToList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<HistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HistoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            dbContext.postureHistory.Add(new PostureState(){quality = PostureQuality.Good, time = DateTime.Now});
            dbContext.SaveChanges();
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
