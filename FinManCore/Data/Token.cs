using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;
namespace FinMan.Data
{
    class TokenManager
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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

            var response = await client.PostAsync(Url, new FormUrlEncodedContent(keyValues));
            var responseMessage = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(responseMessage);

            return token.access_token;
        }
    }


    public class Token
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

}

