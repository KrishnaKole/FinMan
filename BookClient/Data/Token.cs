using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;
namespace FinMan.Data
{
    class Token
    {
        const string Url = "https://dnbapistore.com/token";
        const string Key = "EwcuY9iHOmlRPjHBfAKbebhGEHwa";
        const string Secret = "669JMM4tAYWN5G4waTEIhzOtu3wa";
        public static async Task<string> GetToken()
        {
            HttpClient client = new HttpClient();
            var plainBytes = Encoding.UTF8.GetBytes(Key + ":" + Secret);
            var encodedText = Convert.ToBase64String(plainBytes);

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encodedText);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("UserAgent", "USER_AGENT");


            //var response = await client.PostAsync(Url, new StringContent("grant_type=client_credentials"));
            //return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            return "f5a7683e-f60e-3c18-9903-143f32bd02b3";
        }
    }
}

