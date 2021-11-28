using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostureWebApi.Models;

namespace PostureWebApi.JsonModels
{
    public class AddHistory
    {
        public string UserName { get; set; }
        public PostureQuality quality { get; set; }
    }
}
