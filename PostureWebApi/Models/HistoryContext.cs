using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostureWebApi.Models;

namespace PostureWebApi.DBContexts
{
    public class HistoryContext : DbContext
    {
        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
        {
        }
        public DbSet<PostureState> postureHistory { get; set; }
        public DbSet<User> users { get; set; }

    }
}
