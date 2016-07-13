using System;
using System.Configuration;
using System.IO;
using MagicCardMarket.Log;
using MagicCardMarket.Request.Exceptions;

namespace MagicCardMarket.Request
{
    public class Tokens
    {
        private static readonly Lazy<Tokens> LazyDefault = new Lazy<Tokens>(() => new Tokens());

        public static Tokens Instance => LazyDefault.Value;

        public string AppToken { get; }
        public string AppSecret { get; }
        public string AccessToken { get; }
        public string AccessSecret { get; }
        public string Url = "https://www.mkmapi.eu/ws/v1.1/";

        private Tokens()
        {
            string filename = ConfigurationManager.AppSettings["tokensfile"];
            if (!File.Exists(filename))
                OnError($"Token file {filename} not found");

            string[] lines = File.ReadAllLines(filename);
            foreach (string s in lines)
            {
                if (s.StartsWith("URL="))
                    Url = Clean(s.Split('=')[1]);
                else if (s.StartsWith("App token="))
                    AppToken = Clean(s.Split('=')[1]);
                else if (s.StartsWith("App secret="))
                    AppSecret = Clean(s.Split('=')[1]);
                else if (s.StartsWith("Access token="))
                    AccessToken = Clean(s.Split('=')[1]);
                else if (s.StartsWith("Access token secret="))
                    AccessSecret = Clean(s.Split('=')[1]);
            }

            if (String.IsNullOrWhiteSpace(AppToken))
                OnError($"No apptoken given in {filename}");
            if (String.IsNullOrWhiteSpace(AppSecret))
                OnError($"No appSecret given in { filename}");
            if (String.IsNullOrWhiteSpace(AccessToken))
                OnError($"No accessToken given in {filename}");
            if (String.IsNullOrWhiteSpace(AccessSecret))
                OnError($"No accessSecret given in {filename}");
            if (String.IsNullOrWhiteSpace(Url))
                OnError($"No url given in {filename}");
        }

        private static void OnError(string msg)
        {
            Log.Log.Default.WriteLine(LogLevels.Error, msg);
            throw new TokenException(msg);
        }

        private static string Clean(string text)
        {
            return text.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\"", "").Replace("'", "").Trim();
        }
    }
}
