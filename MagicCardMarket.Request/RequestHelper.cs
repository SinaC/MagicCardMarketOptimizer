using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Cache;

namespace MagicCardMarket.Request
{
    //https://github.com/martsve/mkm_api
    public class RequestHelper
    {
        private readonly ICache _cache = new FileSystemCache(@"d:\temp\MCMOCache");

        public T GetData<T>(string resource)
        {
            string responseRaw = String.Empty;
            try
            {
                responseRaw = MakeRequest(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XElement rootElement = responseXml.Root.Elements().First(); // remove <response>
                return (T) serializer.Deserialize(rootElement.CreateReader());
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(responseRaw);
                throw;
            }
        }

        public T[] GetDatas<T>(string resource)
        {
            string responseRaw = String.Empty;
            try
            {
                responseRaw = MakeRequest(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T[] values = new T[responseXml.Root.Nodes().Count()];
                int index = 0;
                foreach (XNode node in responseXml.Root.Nodes()) // loop on <response> subnodes
                {
                    values[index] = (T) serializer.Deserialize(node.CreateReader());
                    index++;
                }
                return values;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(responseRaw);
                throw;
            }
        }

        public async Task<T> GetDataAsync<T>(string resource, bool useCache = false)
        {
            string responseRaw = null;
            try
            {
                if (useCache)
                    responseRaw = await MakeRequestAsyncWithCache(resource);
                else
                    responseRaw = await MakeRequestAsync(resource);

                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XElement rootElement = responseXml.Root.Elements().First(); // remove <response>
                return (T) serializer.Deserialize(rootElement.CreateReader());
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(responseRaw);
                throw;
            }
        }

        public async Task<T[]> GetDatasAsync<T>(string resource, bool useCache = false)
        {
            string responseRaw = null;
            try
            {
                if (useCache)
                    responseRaw = await MakeRequestAsyncWithCache(resource);
                else
                    responseRaw = await MakeRequestAsync(resource);

                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T[] values = new T[responseXml.Root.Nodes().Count()];
                int index = 0;
                foreach (XNode node in responseXml.Root.Nodes()) // loop on <response> subnodes
                {
                    values[index] = (T) serializer.Deserialize(node.CreateReader());
                    index++;
                }
                return values;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(responseRaw);
                throw;
            }
        }

        public async Task<string> MakeRequestAsyncWithCache(string resource)
        {
            string responseRaw;
            string[] tokens = resource.Split('/');
            if (_cache.Contains(tokens[0], Convert.ToInt32(tokens[1])))
                responseRaw = _cache.Get(tokens[0], Convert.ToInt32(tokens[1]));
            else
            {
                RequestHelper request = new RequestHelper();
                responseRaw = await request.MakeRequestAsync(resource);
                _cache.Set(tokens[0], Convert.ToInt32(tokens[1]), responseRaw);
            }
            return responseRaw;
        }

        public async Task<string> MakeRequestAsync(string resource, string method = "GET", string postData = "")
        {
            string url = Tokens.Url + resource;

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader(method, url));
            request.Method = method;

            if (postData.Length > 0 && (method == "POST" || method == "PUT"))
            {
                // TODO: async
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(postData);
                using (Stream stream = await request.GetRequestStreamAsync())
                {
                    soapEnvelopeXml.Save(stream);
                }

                request.ContentType = "application/xml;charset=\"utf-8\"";
                request.Accept = "application/json,application/xml";
            }

            try
            {
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                return StreamToString(response.GetResponseStream());
            }
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                throw;
            }
        }

        public string MakeRequest(string resource, string method = "GET", string postData = "")
        {
            string url = Tokens.Url + resource;

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader(method, url));
            request.Method = method;

            if (postData.Length > 0 && (method == "POST" || method == "PUT"))
            {
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(postData);
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                request.ContentType = "application/xml;charset=\"utf-8\"";
                request.Accept = "application/json,application/xml";
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return StreamToString(response.GetResponseStream());
            }
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                throw;
            }
        }

        public string MakeRequestPaging(string resource, string method = "GET", string postData = "")
        {
            int start = 1;
            StringBuilder result = new StringBuilder();
            while (true)
            {
                string url = Tokens.Url + resource + "/"+start;

                ServicePointManager.Expect100Continue = false;

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                OAuthHeader header = new OAuthHeader();
                request.Headers.Add(HttpRequestHeader.Authorization, header.GetAuthorizationHeader(method, url));
                request.Method = method;

                if (postData.Length > 0 && (method == "POST" || method == "PUT"))
                {
                    XmlDocument soapEnvelopeXml = new XmlDocument();
                    soapEnvelopeXml.LoadXml(postData);
                    using (Stream stream = request.GetRequestStream())
                    {
                        soapEnvelopeXml.Save(stream);
                    }

                    request.ContentType = "application/xml;charset=\"utf-8\"";
                    request.Accept = "application/json,application/xml";
                }

                try
                {
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    string data = StreamToString(response.GetResponseStream())
                        .Replace(@"<response>", String.Empty)
                        .Replace(@"</response>", String.Empty)
                        .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", String.Empty);
                    result.Append(data);
                    if (response.StatusCode == HttpStatusCode.NoContent)
                        break;
                    else if (response.StatusCode == HttpStatusCode.PartialContent)
                    {
                        string contentRange = response.Headers[HttpResponseHeader.ContentRange]; // 1-100/841
                        string[] tokens = contentRange.Split('-', '/');
                        if (tokens[1] == tokens[2])
                            break;
                    }
                    start += 100;
                }
                catch (WebException ex)
                {
                    if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedException("Unauthorized access Magic Card Market. Check your token file.", ex);
                    throw;
                }
            }
            result.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\"?>"+Environment.NewLine+ "<result>" + Environment.NewLine);
            result.AppendLine("</result>");
            return result.ToString();
        }

        private static string StreamToString(Stream input)
        {
            StreamReader reader = new StreamReader(input, Encoding.UTF8);
            return reader.ReadToEnd();
        }

    }
}
