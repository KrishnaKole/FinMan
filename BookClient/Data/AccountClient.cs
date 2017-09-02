using System;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace BookClient.Data
{
    class AccountClient
    {
        const string Url = "https://dnbapistore.com/hackathon/accounts/1.0/account/";
        private string authorizationKey;
        public async Task<CustomerAccount> GetAll(string customerId)
        {
            var client = await GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(result);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
            Book book = new Book { Title = title, Authors = new List<string>(new[] { author }), Genre = genre, PublishDate = DateTime.Now, ISBN = "" };
            var client = await GetClient();
            var response = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

        }

        }
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            if (String.IsNullOrEmpty(authorizationKey))
            {
                var token = await client.GetStringAsync(Url + "login");
                authorizationKey = JsonConvert.DeserializeObject<string>(token);

            }
            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
