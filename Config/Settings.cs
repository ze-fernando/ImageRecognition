using DotNetEnv;

namespace ImageRecognition.Config;

public static class Settings{
    public static string Connection = Env.GetString("DATABAE_CONNECTION");

    public static string GeminiKey = Env.GetString("GEMINI_KEY");
}