using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;
using static System.Console;

namespace SD3_Tg_Bot
{
    internal class GenerateSettings
    {
        private static readonly HttpClient client = new HttpClient();
        public string prompt { get; set; }
        public string negativePrompt { get; set; }
        public int seed { get; set; }
        public bool randomizeSeed { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double guidanceScale { get; set; }
        public int numInferenceSteps { get; set; }


        public GenerateSettings() 
        { 
        
        }

        public static async Task<string> GenerateByPrompt()
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
            WriteLine(response);
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
    }
}
