using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static System.Console;

namespace SD3_Tg_Bot
{
    public class DB
    {
        private readonly Message _messageUser;
        //private bool _userExist;
        public DB(Message message) { _messageUser = message; }

        //проверка пользователя на наличие в бд
        private async Task <bool> UserExists()
        {

            return false;//TODO: реализовать проверку на наличие в бд юзера
        }
        //добавление пользователя в базу
        public async Task AddNewUser()
        {
            User newUser = new();
            //проверка пользователя на существование в бд
            if (! await UserExists())
            {
                //проверка на null перед добавлением в бд
                if (_messageUser?.From?.Id != null && _messageUser?.From?.Username!=null )
                {
                    //TODO: пока не продумал логику отслеживания чата с меню, надо будет переделывать
                    newUser.TgMenuMessageId = 1;
                    newUser.TgUserId = _messageUser.From.Id;
                    newUser.TgUserName = _messageUser.From.Username;
                }
                else
                {
                    WriteLine($"{nameof(UserExists)}  Null");
                }
            }

        }
        public class User
        {
            //первичный ключ и без инкремента
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public long? TgUserId { get; set; }
            public string? TgUserName { get; set; }
            
            public long TgMenuMessageId { get; set; }
            
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
