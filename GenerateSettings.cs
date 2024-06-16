namespace SD3_Tg_Bot
{
    internal class GenerateSettings
    {
        public string prompt { get; set; }
        public string negativePrompt { get; set; }
        public string seed { get; set; }
        public bool randomizeSeed { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double guidanceScale { get; set; }
        public int numInferenceSteps { get; set; }


        public GenerateSettings() { }

    }
}
