using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD3_Tg_Bot
{
    public class DB
    {
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; }
        }
        public class ApplicationContext : DbContext
        {
            public DbSet<User> Users { get; set; } = null!;

            public ApplicationContext()
            {
                Database.EnsureCreated();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testTgDB;Username=postgres;Password=1452");
            }
        }
    }
}
