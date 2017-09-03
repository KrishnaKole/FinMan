using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace FinMan.Data
{
    class Utils
    {
        static string authorizationKey;
        public static async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();

            if (String.IsNullOrEmpty(authorizationKey))
            {
                authorizationKey = await TokenManager.GetToken();
            }
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }

            return response;
        }
    }
}
