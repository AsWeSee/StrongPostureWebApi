using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostureWebApi.JsonModels
{
    public class ValidatePoint
    {
        public string UserName { get; set; }
        public float[] Points { get; set; }
    }
}
