using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Log;
using MagicCardMarket.Request.Exceptions;

namespace MagicCardMarket.Request
{
    public class GetPagingRequestHelper : IGetRequestHelper
    {
        private const int PageSize = 100;

        #region IGetRequestHelper

        public XDocument Get(string request)
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> GetAsync(string request)
        {
            using (new LogExecutionTime($"GetAsync: request={request}"))
            {
                int start = 1;
                StringBuilder result = new StringBuilder();
                while (true)
                {
                    string url = Tokens.Instance.Url + request + "/" + start;

                    ServicePointManager.Expect100Continue = false;

                    OAuthHeader header = new OAuthHeader();
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                    webRequest.Method = "GET";
                    webRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));

                    try
                    {
                        HttpWebResponse webResponse = (HttpWebResponse)(await webRequest.GetResponseAsync());
                        string data = StreamToString(webResponse.GetResponseStream());
                        result.Append(data);
                        if (webResponse.StatusCode == HttpStatusCode.NoContent)
                            break;
                        if (webResponse.StatusCode == HttpStatusCode.OK)
                            break;
                        if (webResponse.StatusCode == HttpStatusCode.PartialContent)
                        {
                            string contentRange = webResponse.Headers[HttpResponseHeader.ContentRange]; // 1-100/841
                            string[] tokens = contentRange.Split('-', '/');
                            if (tokens[1] == tokens[2])
                                break;
                        }
                        start += PageSize;
                    }
                    catch (WebException ex)
                    {
                        Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
                        if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                            throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                        if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode)429)
                            throw new TooManyRequestsException("Too many requests for today. Wait until 12am CET.", ex);
                        throw;
                    }
                }
                if (start > 1) /*only if more than one page*/
                {
                    // Remove every response/xml tag
                    result
                        .Replace(@"<response>", String.Empty)
                        .Replace(@"</response>", String.Empty)
                        .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", String.Empty);
                    // Add xml/response tag
                    result.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<result>" + Environment.NewLine);
                    result.AppendLine("</result>");
                }
                return XDocument.Load(new StreamReader(result.ToString()));
            }
        }

        #endregion

        private static string StreamToString(Stream input)
        {
            StreamReader reader = new StreamReader(input, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
