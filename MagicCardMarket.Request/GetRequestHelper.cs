using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Log;
using MagicCardMarket.Request.Exceptions;

namespace MagicCardMarket.Request
{
    public class GetRequestHelper : IGetRequestHelper
    {
        #region IGetRequestHelper

        public XDocument Get(string request)
        {
            using (new LogExecutionTime($"Getc: request={request}"))
            {
                string url = Tokens.Instance.Url + request;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                OAuthHeader header = new OAuthHeader();
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));
                httpWebRequest.Method = "GET";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                    Log.Log.Default.WriteLine(LogLevels.Info, $"X-Request-Limit: {response.Headers["X-Request-Limit-Count"]}/{response.Headers["X-Request-Limit-Max"]}");
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

        public async Task<XDocument> GetAsync(string request)
        {
            using (new LogExecutionTime($"GetAsync: request={request}"))
            {
                string url = Tokens.Instance.Url + request;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                OAuthHeader header = new OAuthHeader();
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));
                httpWebRequest.Method = "GET";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                    Log.Log.Default.WriteLine(LogLevels.Info, $"X-Request-Limit: {response.Headers["X-Request-Limit-Count"]}/{response.Headers["X-Request-Limit-Max"]}");
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

        #endregion
    }
}
