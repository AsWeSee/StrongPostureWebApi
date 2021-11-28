using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostureWebApi.JsonModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostureWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private static Dictionary<string, Classification.Classification> classifications;

        public ClassificationController() : base()
        {
            if (classifications == null)
            {
                classifications = new Dictionary<string, Classification.Classification>();
            }
        }

        // GET: api/<ClassificationController>
        [HttpGet("hasLearnData")]
        public bool Get([FromBody] string user)
        {
            if (classifications.ContainsKey(user))
                return true;
            else
                return false;
        }

        // GET api/<ClassificationController>/5
        [HttpPost("clearLearnData")]
        public bool Post([FromBody] string user)
        {
            if (classifications.ContainsKey(user))
            {
                classifications[user].ResetTrainData();
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST api/<ClassificationController>
        [HttpPost("learn")]
        public string Post([FromBody] LearnPoint point)
        {
            if (!classifications.ContainsKey(point.UserName))
            {
                classifications[point.UserName] = new Classification.Classification();
            }

            return classifications[point.UserName].AddPointsAndReTrain(point);
        }
        // POST api/<ClassificationController>
        [HttpPost("validate")]
        public LearnPrediction ValidatePost([FromBody] LearnPoint point)
        {
            if (classifications.ContainsKey(point.UserName))
            {
                return classifications[point.UserName].Predict(point);
            }
            else
            {
                return new LearnPrediction()
                {
                    Prediction = false,
                    Score = -1
                };
            }
        }

        // DELETE api/<ClassificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
