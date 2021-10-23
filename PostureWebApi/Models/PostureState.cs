using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostureWebApi.Models
{
    public class PostureState
    {
        public int Id { get; set; }
        public PostureQuality quality { get; set; }
        public DateTime time { get; set; }
    }
}
