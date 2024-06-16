using Telegram.Bot.Types.ReplyMarkups;

namespace SD3_Tg_Bot
{
    internal class KeyBoards
    {
        public ReplyKeyboardMarkup MainMenuKeyBoard()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Help me", "Call me ☎️" },
            })
            {
                ResizeKeyboard = true
            };


            return replyKeyboardMarkup;
        }

    }
}
