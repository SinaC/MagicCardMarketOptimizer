using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MagicCardMarket.Request
{
    //https://github.com/martsve/mkm_api
    public class RequestHelper
    {
        public T GetData<T>(string resource)
        {
            string responseRaw = String.Empty;
            try
            {
                RequestHelper request = new RequestHelper();
                responseRaw = request.MakeRequest(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XElement rootElement = responseXml.Root.Elements().First();
                return (T) serializer.Deserialize(rootElement.CreateReader());
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(responseRaw);
                throw;
            }
        }

        public async Task<T> GetDataAsync<T>(string resource)
        {
            string responseRaw = String.Empty;
            try
            {
                RequestHelper request = new RequestHelper();
                responseRaw = await request.MakeRequestAsync(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XElement rootElement = responseXml.Root.Elements().First();
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
                RequestHelper request = new RequestHelper();
                responseRaw = request.MakeRequest(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T[] values = new T[responseXml.Root.Nodes().Count()];
                int index = 0;
                foreach (XNode node in responseXml.Root.Nodes())
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

        public async Task<T[]> GetDatasAsync<T>(string resource)
        {
            string responseRaw = String.Empty;
            try
            {
                RequestHelper request = new RequestHelper();
                responseRaw = await request.MakeRequestAsync(resource);
                XDocument responseXml = XDocument.Parse(responseRaw);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T[] values = new T[responseXml.Root.Nodes().Count()];
                int index = 0;
                foreach (XNode node in responseXml.Root.Nodes())
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
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                request.ContentType = "application/xml;charset=\"utf-8\"";
                request.Accept = "application/json,application/xml";
            }

            HttpWebResponse response;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
                byte[] b = ReadFully(response.GetResponseStream());
                return Encoding.UTF8.GetString(b, 0, b.Length);
            }

            catch (WebException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                response = ex.Response as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine(@"App token = '{0}'
App secret = '{1}'
Access token = '{2}'
Access token secret = '{3}'", Tokens.AppToken, Tokens.AppSecret, Tokens.AccessToken, Tokens.AccessSecret);
                }
                return "";
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return "";
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

            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                byte[] b = ReadFully(response.GetResponseStream());
                return Encoding.UTF8.GetString(b, 0, b.Length);
            }
            catch (WebException ex)
            {
                // TODO: add logging

                //                Console.WriteLine("Error: " + ex.Message);
                //                response = ex.Response as HttpWebResponse;
                //                if (response.StatusCode == HttpStatusCode.Unauthorized)
                //                {
                //                    Console.WriteLine(@"App token = '{0}'
                //App secret = '{1}'
                //Access token = '{2}'
                //Access token secret = '{3}'", Tokens.AppToken, Tokens.AppSecret, Tokens.AccessToken, Tokens.AccessSecret);
                //                }
                //                return "";
                throw;
            }
            catch (Exception ex)
            {
                // TODO: add logging

                //Console.WriteLine("Error: " + ex.Message);
                //return "";
                throw;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
