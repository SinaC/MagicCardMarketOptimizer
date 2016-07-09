using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace MagicCardMarket.App
{
    //https://github.com/martsve/mkm_api
    public class RequestHelper
    {
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
