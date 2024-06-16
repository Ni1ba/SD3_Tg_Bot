using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;


using static SD3_Tg_Bot.KeyBoards;
using static System.Console;
using static System.IO.Path;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    public static TelegramBotClient _botClient;
    public static string _token;
    public static CancellationTokenSource _cancellationTokenSource;
    public static CancellationToken _cancellationToken;

    private static readonly HttpClient client = new HttpClient();
 


    static async Task Main(string[] args)
    {

        //установка токена
        _token = ReturnToken();
        //установка бота
        _botClient = new TelegramBotClient(_token);
        //хз че за отмена токена, но без нее не запускается
        _cancellationTokenSource = new();
        //пока тоже непонятно зачем надо,но в доках было
        var me = await _botClient.GetMeAsync();
        //просто пока тоже должно быть
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() //получать все типы обновлений, кроме обновлений, связанных с ChatMember
        };
        //собственно пуск бота
        _botClient.StartReceiving
            (updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: _cancellationTokenSource.Token
            );


        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        //WelcomeMsg();

        //остановка бота
        _cancellationTokenSource.Cancel();


        
        ////usePrompt();
    }
    static async Task WelcomeMsg()
    {

        //Message sentMessage = await _botClient.SendTextMessageAsync(
        //chatId: chatId,
        //    text: "Choose a response",
        //    replyMarkup: replyKeyboardMarkup,
        //    cancellationToken: cancellationToken);
    }

    //Обработчик введенного пользователем текста
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        if (message.Text == "hi")
        {
            SentSimpleMsg(message,"кукусики");
        }

        //// Echo  
        //Message sentMessage = await _botClient.SendTextMessageAsync(
        //    chatId: chatId,
        //    text: "You said:\n" + messageText,
        //    cancellationToken: cancellationToken);
    }


    //отправка простого сообщения пользователю
    static async Task SentSimpleMsg(Message messageFromUser, string messageToUser)
    {
        var chatId = messageFromUser.Chat.Id;
        Message sentMessage = new();
        
        //WriteLine($"Received a '{messageFromUser.Text}' message in chat {chatId}.");

        sentMessage.Text = messageToUser;
        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: messageToUser,
            cancellationToken: _cancellationToken
            );

    }

    static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    public static string ReturnToken()
    {
        //  TODO: сделать поиск файла с токеном, пока просто абсолютный путь есть 
        string TokenPath = @"C:\dev\projects\c#\Pets\Bot_SD3\SD3_Tg_Bot\TOKEN.txt";
        StreamReader Token = new StreamReader(TokenPath);
        string line = Token.ReadLine();
        return line;
    }

    public static async Task<string> usePrompt()
    {
        var url = "http://127.0.0.1:5000/predict"; // Убедитесь, что URL правильный
        var data = new
        {
            prompt = "Hello!!",
            negative_prompt = "Hello!!",
            seed = 0,
            randomize_seed = true,
            width = 1024,
            height = 1024,
            guidance_scale = 5,
            num_inference_steps = 28
        };

        var response = await CallApi(url, data);
        Console.WriteLine(response);
        return null;
    }

    public static async Task<string> CallApi(string url, object data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }

    static string FindCsProjFile(string directory)
    {
        string[] csprojFiles = System.IO.Directory.GetFiles(directory, "*.csproj");

        if (csprojFiles.Length > 0)
        {
            return csprojFiles[0]; // Возвращаем первый  файл 
        }
        else
        {
            // Если файл .csproj не найден в текущей директории, ищем в родительской директории
            string parentDirectory = System.IO.Directory.GetParent(directory)?.FullName;
            if (parentDirectory != null)
            {
                return FindCsProjFile(parentDirectory); // Рекурсивно ищем в родительской директории
            }
            else
            {
                return null; // Не нашел файл .csproj
            }
        }
    }
}
