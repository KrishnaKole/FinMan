using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace FinMan.Data
{
    class AccountClient
    {
        const string Url = "https://dnbapistore.com/hackathon/accounts/1.0/account/";
        public async Task<CustomerAccount> GetAll(string customerId)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "customer/" + customerId);
            try
            {
                return JsonConvert.DeserializeObject<CustomerAccount>(result);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

    }
}
