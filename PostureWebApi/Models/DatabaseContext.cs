//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace PostureWebApi.Models
//{

//    public class DatabaseContext : DbContext
//    {
//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//            options.UseSqlite("Data Source=entertainment.db");
//        }
//        public DbSet<PostureState> postureHistory { get; set; }

//        public DatabaseContext()
//        {
//            Database.EnsureCreated();   // создаем базу данных при первом обращении
//        }




//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}
