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
                string url = Tokens.Url + request;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                OAuthHeader header = new OAuthHeader();
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));
                httpWebRequest.Method = "GET";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                    return XDocument.Load(new StreamReader(response.GetResponseStream()));
                }
                catch (WebException ex)
                {
                    Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
                    if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                    if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException("Too many requests for today. Wait until 12am CET", ex);
                    throw;
                }
            }
        }

        public async Task<XDocument> GetAsync(string request)
        {
            using (new LogExecutionTime($"GetAsync: request={request}"))
            {
                string url = Tokens.Url + request;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                OAuthHeader header = new OAuthHeader();
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));
                httpWebRequest.Method = "GET";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                    return XDocument.Load(new StreamReader(response.GetResponseStream()));
                }
                catch (WebException ex)
                {
                    Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
                    if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                    if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode)429)
                        throw new TooManyRequestsException("Too many requests for today. Wait until 12am CET", ex);
                    throw;
                }
            }
        }

        #endregion
    }
}
