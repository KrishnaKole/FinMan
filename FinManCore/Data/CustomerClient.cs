using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace FinMan.Data
{
    class CustomerClient
    {
        const string Url = "https://dnbapistore.com/hackathon/customers/1.0/customer";
        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest customer)
        {
            var client = await Utils.GetClient();
            var result = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<CustomerResponse>(await result.Content.ReadAsStringAsync());
        }

        public async Task<UpdateCustomerRequestResponse> UpdateCustomer(string customerId, string phoneNumber,string email, string street, string postalCode, string city, string country)
        {
            UpdateCustomerRequestResponse customer = new UpdateCustomerRequestResponse { customerID = customerId, email = email, phoneNumber = phoneNumber, address = new Address { street = street, postalCode = postalCode, city = city, country = country } };
            var client = await Utils.GetClient();
            var result = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<UpdateCustomerRequestResponse>(await result.Content.ReadAsStringAsync());
        }


        public async Task<CustomerResponse> GetCustomerAccountDetails(string customerId)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + $"/{customerId}");
            return JsonConvert.DeserializeObject<CustomerResponse>(result);
        }
    }
}
