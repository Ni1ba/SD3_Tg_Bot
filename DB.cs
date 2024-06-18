using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telegram.Bot.Types;
using static SD3_Tg_Bot.DB;
using static System.Console;

namespace SD3_Tg_Bot
{
    public class DB
    {
        private readonly Message _messageUser;
        private readonly Message _messageBot;

        public DB(Message message) { _messageUser = message; }
        public DB(Message userMessage,Message botMessage) 
        {
            _messageUser = userMessage;
            _messageBot = botMessage;
        }

        //обновление данных по юзеру
        public async Task UpdateUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!await UserExistsInDb())
                {

                }
                else
                {
                    WriteLine($"{_messageUser.From.Username} не существует в базе, обновление не выполнено");
                }
            }
        }

        //проверка пользователя на наличие в бд
        private async Task<bool> UserExistsInDb()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (_messageUser?.From?.Id != null)
                {
                    if (await db.Users.FindAsync(_messageUser.From.Id) != null)
                    { return true; }
                }
            }
            return false;
        }
        //получение пользователя из базы
        public async Task <User> GetUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User newUser = new();
                if (await UserExistsInDb())
                {
                    return await db.Users.FindAsync(_messageUser.From.Id);
                }
                else
                {
                    return null ;
                }

            }
        }

       

        //добавление пользователя в базу
        public async Task AddNewUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User newUser = new();
                //проверка пользователя на существование в бд
                if (!await UserExistsInDb())
                {
                    //проверка на null перед добавлением в бд
                    if (_messageUser?.From?.Id != null && _messageUser?.From?.Username != null)
                    {
                        //TODO: пока не продумал логику отслеживания чата с меню, надо будет переделывать
                        newUser.TgMenuMessageId = 0;
                        newUser.TgUserId = _messageUser.From.Id;
                        newUser.TgUserName = _messageUser.From.Username;
                        db.Users.Add(newUser);
                        await db.SaveChangesAsync();
                        WriteLine($"User {_messageUser.From.Username} добавлен");
                    }
                    else
                    {
                        WriteLine($"{nameof(UserExistsInDb)}  Null");
                    }
                }
                else
                {
                    WriteLine($"User {_messageUser.From.Username} уже существует в бд");
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
            public DateTime DateTimeMessageMenu { get; set; }
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
