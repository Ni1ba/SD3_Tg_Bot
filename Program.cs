using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using System.IO;


using static System.Console;
using static System.IO.Path;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        // Получаем путь к текущему исполняемому файлу
        string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        // Получаем директорию, в которой находится текущий исполняемый файл
        string projectDirectory = GetDirectoryName(executablePath);

        // Ищем файл .csproj в текущей директории и во всех родительских директориях
        string csprojFile = FindCsProjFile(projectDirectory);
        
        Console.WriteLine("Путь к файлу .csproj: " + csprojFile);
        //usePrompt();
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
