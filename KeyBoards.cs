using Telegram.Bot.Types.ReplyMarkups;

namespace SD3_Tg_Bot
{
    internal class KeyBoards
    {
        public ReplyKeyboardMarkup ReplyMainMenuKeyBoard()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Промпт", "Негативный промпт" },
                new KeyboardButton[] { "Генерация" }
            }
            )
            {
                ResizeKeyboard = true
            };
            return replyKeyboardMarkup;
        }

        public InlineKeyboardMarkup InlineMainMenuKeyBoard()
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
             {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Промпт", callbackData: "prompt"),
                    InlineKeyboardButton.WithCallbackData(text: "Негативный промпт", callbackData: "negativePrompt"),
                },
                // second row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Генерация", callbackData: "generate"),
                    //InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
                },
            });

            return inlineKeyboard;
        }

    }
}
