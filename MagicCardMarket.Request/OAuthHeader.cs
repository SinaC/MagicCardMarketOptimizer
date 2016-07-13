using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MagicCardMarket.Request
{
    //https://www.mkmapi.eu/ws/documentation/API:Auth_csharp
    //https://github.com/martsve/mkm_api
    /// <summary>
    /// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
    /// </summary>
    public class OAuthHeader
    {
        /// <summary>OAuth Signature Method</summary>
        protected const string SignatureMethod = "HMAC-SHA1";
        /// <summary>OAuth Version</summary>
        protected const string Version = "1.0";
        /// <summary>All Header params compiled into a Dictionary</summary>
        protected IDictionary<string, string> HeaderParams;

        /// <summary>
        /// Constructor
        /// </summary>
        public OAuthHeader()
        {
            string nonce = Guid.NewGuid().ToString("n");
            //string nonce = "53eb1f44909d6";
            string timestamp = ((int)((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)).ToString();
            //string timestamp = "1407917892";

            HeaderParams = new Dictionary<string, string>
            {
                { "oauth_consumer_key",  Tokens.Instance.AppToken},
                { "oauth_token",  Tokens.Instance.AccessToken},
                { "oauth_nonce", nonce},
                { "oauth_timestamp", timestamp},
                { "oauth_signature_method", SignatureMethod},
                { "oauth_version", Version}
            };
        }


        /// <summary>
        /// Pass request method and URI parameters to get the Authorization header value
        /// </summary>
        /// <param name="method">Request Method</param>
        /// <param name="url">Request URI</param>
        /// <returns>Authorization header value</returns>
        public string GetAuthorizationHeader(string method, string url)
        {
            string[] uriParts = url.Split('?');
            string baseUri = uriParts[0];

            // Add the realm parameter to the header params
            //string realm = new Uri(url).Host;
            string realm = baseUri;

            HeaderParams.Add("realm", realm);

            // Start composing the base string from the method and request URI
            string baseString = method.ToUpper()
                              + "&"
                              + Uri.EscapeDataString(baseUri)
                              + "&";


            string[] requestParameters = uriParts.Length > 1 ? uriParts[1].Split('&') : new string[] { };
            foreach (var parameter in requestParameters)
            {
                var parts = parameter.Split('=');
                string key = parts[0];
                string value = parts.Length > 1 ? parts[1] : "";
                HeaderParams.Add(key, value);
            }


            // Gather, encode, and sort the base string parameters
            SortedDictionary<string, string> encodedParams = new SortedDictionary<string, string>();
            foreach (var parameter in HeaderParams)
            {
                if (!parameter.Key.Equals("realm"))
                {
                    encodedParams.Add(Uri.EscapeDataString(parameter.Key), Uri.EscapeDataString(parameter.Value));
                }
            }

            // Expand the base string by the encoded parameter=value pairs
            List<string> paramStrings = new List<string>();
            foreach (KeyValuePair<string, string> parameter in encodedParams)
            {
                paramStrings.Add(parameter.Key + "=" + parameter.Value);
            }
            string paramString = Uri.EscapeDataString(string.Join<string>("&", paramStrings));
            baseString += paramString;

            // Create the OAuth signature
            string signatureKey = Uri.EscapeDataString(Tokens.Instance.AppSecret) + "&" + Uri.EscapeDataString(Tokens.Instance.AccessSecret);
            HMAC hasher = HMAC.Create();
            hasher.Key = Encoding.UTF8.GetBytes(signatureKey);
            Byte[] rawSignature = hasher.ComputeHash(Encoding.UTF8.GetBytes(baseString));
            string oAuthSignature = Convert.ToBase64String(rawSignature);

            // Include the OAuth signature parameter in the header parameters array
            HeaderParams.Add("oauth_signature", oAuthSignature);

            // Construct the header string
            List<string> headerParamStrings = new List<string>();
            foreach (KeyValuePair<string, string> parameter in HeaderParams)
            {
                headerParamStrings.Add(parameter.Key + "=\"" + parameter.Value + "\"");
            }
            string authHeader = "OAuth " + string.Join<string>(", ", headerParamStrings);

            return authHeader;
        }
    }
}
