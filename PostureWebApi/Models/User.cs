using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostureWebApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string HeroName { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
    }
}
