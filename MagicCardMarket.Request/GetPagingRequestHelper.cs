﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MagicCardMarket.Log;
using MagicCardMarket.Request.Exceptions;

namespace MagicCardMarket.Request
{
    public class GetPagingRequestHelper : IGetRequest, IGetPagingRequest
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
                    HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
                    webRequest.Method = "GET";
                    webRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));

                    try
                    {
                        HttpWebResponse webResponse = (HttpWebResponse) await webRequest.GetResponseAsync();
                        Log.Log.Default.WriteLine(LogLevels.Debug, $"X-Request-Limit: {webResponse.Headers["X-Request-Limit-Count"]}/{webResponse.Headers["X-Request-Limit-Max"]}");
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
                            throw new UnauthorizedException("Unauthorized access. Check your token file.", ex);
                        if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode) 429)
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
                    // Add one global xml/response tag
                    result.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<result>" + Environment.NewLine);
                    result.AppendLine("</result>");
                }
                return XDocument.Parse(result.ToString());
            }
        }

        #endregion

        #region IGetPagingRequestHelper

        public PagedResult Get(string request, int from)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult> GetAsync(string request, int from)
        {
            using (new LogExecutionTime($"GetAsync: request={request} from={from}"))
            {
                string url = Tokens.Instance.Url + request + "/" + from;

                ServicePointManager.Expect100Continue = false;

                OAuthHeader header = new OAuthHeader();
                HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader("GET", url));

                try
                {
                    HttpWebResponse webResponse = (HttpWebResponse) await webRequest.GetResponseAsync();

                    string contentRange = webResponse.Headers[HttpResponseHeader.ContentRange]; // 1-100/841
                    string[] tokens = contentRange.Split('-', '/');
                    int currentIndex = SaveConvertToInt(tokens[0] ?? String.Empty);
                    int maxIndex = SaveConvertToInt(tokens[1] ?? String.Empty);

                    Log.Log.Default.WriteLine(LogLevels.Debug, $"X-Request-Limit: {webResponse.Headers["X-Request-Limit-Count"]}/{webResponse.Headers["X-Request-Limit-Max"]}");

                    XDocument data = XDocument.Load(new StreamReader(webResponse.GetResponseStream()));
                    if (webResponse.StatusCode == HttpStatusCode.NoContent)
                        return new PagedResult
                        {
                            Data = data,
                            CurrentIndex = 0,
                            MaxIndex = 0,
                            IsComplete = true
                        };
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                        return new PagedResult
                        {
                            Data = data,
                            CurrentIndex = currentIndex,
                            MaxIndex = maxIndex,
                            IsComplete = true
                        };
                    if (webResponse.StatusCode == HttpStatusCode.PartialContent)
                        return new PagedResult
                        {
                            Data = data,
                            CurrentIndex = currentIndex,
                            MaxIndex = maxIndex,
                            IsComplete = currentIndex == maxIndex
                        };
                    return new PagedResult
                    {
                        Data = data,
                        CurrentIndex = currentIndex,
                        MaxIndex = maxIndex,
                        IsComplete = false
                    };
                }
                catch (WebException ex)
                {
                    Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
                    if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedException("Unauthorized access. Check your token file.", ex);
                    if ((ex.Response as HttpWebResponse)?.StatusCode == (HttpStatusCode) 429)
                        throw new TooManyRequestsException("Too many requests for today. Wait until 12am CET.", ex);
                    throw;
                }
            }
        }

        #endregion

        private static string StreamToString(Stream input)
        {
            StreamReader reader = new StreamReader(input, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private static int SaveConvertToInt(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return -1;
            int result;
            bool parsed = int.TryParse(input, out result);
            return parsed ? result : -1;
        }
    }
}
