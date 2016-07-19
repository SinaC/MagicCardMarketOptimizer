using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Log;
using MagicCardMarket.Request.Exceptions;

namespace MagicCardMarket.Request
{
    public class PostRequestHelper : IPostRequestHelper
    {
        public XDocument Post(string request, XDocument data)
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> PostAsync(string request, XDocument data)
        {
            using (new LogExecutionTime($"PostAsync: request={request}"))
            {
                string url = Tokens.Instance.Url + request;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                OAuthHeader header = new OAuthHeader();
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("POST", url));
                httpWebRequest.Method = "POST";

                using (Stream stream = await httpWebRequest.GetRequestStreamAsync())
                {
                    data.Save(stream);
                }
                httpWebRequest.ContentType = "application/xml;charset=\"utf-8\"";
                httpWebRequest.Accept = "application/json,application/xml";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                    Log.Log.Default.WriteLine(LogLevels.Debug, $"X-Request-Limit: {response.Headers["X-Request-Limit-Count"]}/{response.Headers["X-Request-Limit-Max"]}");
                    return XDocument.Load(new StreamReader(response.GetResponseStream()));
                }
                catch (WebException ex)
                {
                    Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
                    if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedException("Unauthorized access. Check your token file.", ex);
                    if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException("Too many requests for today. Wait until 12am CET.", ex);
                    throw;
                }
            }
        }
    }
}
