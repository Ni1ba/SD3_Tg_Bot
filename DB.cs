using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD3_Tg_Bot
{
    public class DB
    {
        public class User
        {
            [Key]
            public long? TgUserId { get; set; }
            public string? TgUserName { get; set; }
            public long? TgChatId { get; set; }
            public long? TgMenuMessageId { get; set; }
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
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SD3TgBot;Username=postgres;Password=1452");
            }
        }
    }
}
