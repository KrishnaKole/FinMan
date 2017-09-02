using System;
using System.Collections.Generic;
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
}
