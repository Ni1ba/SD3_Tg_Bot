using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json;
using static System.Console;

namespace SD3_Tg_Bot
{
    internal class GenerateSettings
    {
        private static readonly HttpClient client = new HttpClient();
        public static string _prompt { get; set; }
        public static string? _negativePrompt { get; set; }
        public static int _seed { get; set; }
        public static bool _randomizeSeed { get; set; }
        public static int _width { get; set; }
        public static int _height { get; set; }
        public static double _guidanceScale { get; set; }
        public static int _numInferenceSteps { get; set; }


        public GenerateSettings() 
        {
            _prompt = "Hello!!";
            _negativePrompt = "";
            _seed = 0;
            _randomizeSeed = true;
            _width = 1024;
            _height = 1024;
            _guidanceScale = 5.0;
            _numInferenceSteps = 20;
        }

        public static async Task<string> GenerateByPrompt()
        {
            var url = "http://127.0.0.1:5000/predict"; // Убедитесь, что URL правильный
            var data = new
            {
                prompt = _prompt,
                negative_prompt = _negativePrompt,
                seed = _seed,
                randomize_seed = _randomizeSeed,
                width = _width,
                height = _height,
                guidance_scale = _guidanceScale,
                num_inference_steps = _numInferenceSteps
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
