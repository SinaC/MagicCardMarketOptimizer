using System;
using System.IO;

namespace MagicCardMarketOptimizer
{
    //https://github.com/martsve/mkm_api
    public static class Tokens
    {
        public static string AppToken = "";
        public static string AppSecret = "";
        public static string AccessToken = "";
        public static string AccessSecret = "";
        public static string Url = "https://www.mkmapi.eu/ws/v1.1/";

        public static void Init(string filename = "tokens.txt")
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string s in lines)
            {
                if (s.StartsWith("URL="))
                    Url = Clean(s.Split('=')[1]);
                if (s.StartsWith("App token="))
                    AppToken = Clean(s.Split('=')[1]);
                if (s.StartsWith("App secret="))
                    AppSecret = Clean(s.Split('=')[1]);
                if (s.StartsWith("Access token="))
                    AccessToken = Clean(s.Split('=')[1]);
                if (s.StartsWith("Access token secret="))
                    AccessSecret = Clean(s.Split('=')[1]);
            }

            if (AppToken.Length == 0) Error("No apptoken given in " + filename);
            if (AppSecret.Length == 0) Error("No appSecret given in " + filename);
            if (AccessToken.Length == 0) Error("No accessToken given in " + filename);
            if (AccessSecret.Length == 0) Error("No accessSecret given in " + filename);
            if (Url.Length == 0) Error("No url given in " + filename);
        }

        private static string Clean(string text)
        {
            return text.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "").Replace("'", "").Trim();
        }

        private static void Error(string msg)
        {
            Console.WriteLine("Error: " + msg);
            Environment.Exit(1);
        }
    }
}
