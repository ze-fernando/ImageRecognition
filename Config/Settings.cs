using DotNetEnv;

namespace ImageRecognition.Config;

public static class Settings{
    public static string Connection = Env.GetString("DATABAE_CONNECTION");
}