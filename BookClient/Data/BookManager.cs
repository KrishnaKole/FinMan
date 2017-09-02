using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FinMan.Data;

namespace FinMan.Data
{
    public class BookManager
    {
        const string Url = "http://xam150.azurewebsites.net/api/books/";
        private string authorizationKey;
        public async Task<IEnumerable<Book>> GetAll()
        {
            var client = await GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(result);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
            Book book = new Book { Title = title, Authors = new List<string> ( new[] { author } ), Genre = genre, PublishDate = DateTime.Now, ISBN = "" };
            var client = await GetClient();
            var response = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

        }

        public async Task Update(Book book)
        {
            var client = await GetClient();
            await client.PutAsync(Url + book.ISBN, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "applicayion/json"));
        }

        public async Task Delete(string isbn)
        {
            var client = await GetClient();
            await client.DeleteAsync(Url + isbn);
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

