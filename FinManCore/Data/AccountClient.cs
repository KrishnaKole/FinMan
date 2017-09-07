using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace FinMan.Data
{
    public class AccountClient
    {
        const string Url = "https://dnbapistore.com/hackathon/accounts/1.0/account";
        public async Task<AccountCustomer> GetAllAccounts(string customerId)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "/customer/" + customerId);
            return JsonConvert.DeserializeObject<AccountCustomer>(result);
        }

        public async Task<IEnumerable<CustomerAccount>> GetAllAccountDetails(string customerId)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "/customer/" + customerId);
            var accountCustomer = JsonConvert.DeserializeObject<AccountCustomer>(result);
            List<CustomerAccount> accountDetails = new List<CustomerAccount>();

            foreach (var account in accountCustomer.accounts)
            {
                var details = await GetCustomerAccountDetails(customerId, account.accountNumber.ToString());
                accountDetails.Add(details);
            }
            return accountDetails;
        }

        public async Task<AccountResponse> CreateAccount(string customerId, string name, string type, string currency)
        {
            var account = new CreateAccountRequest { customerID = customerId, accountCurrency = currency, accountName = name, accountType = type };
            var client = await Utils.GetClient();
            var result = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<AccountResponse>(await result.Content.ReadAsStringAsync());
        }

        public async Task<AccountResponse> CloseAccount(string customerId, string accountNumber)
        {
            var account = new CloseAccountRequest { customerID = customerId, accountNumber = accountNumber };
            var client = await Utils.GetClient();
            var result = await client.PatchAsync(Url, new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<AccountResponse>(await result.Content.ReadAsStringAsync());
        }

        public async Task<UpdateAccountNameResponse> UpdateAccountName(string customerId, string accountNumber, string accountName)
        {
            var account = new UpdateAccountNameRequest { customerID = customerId, accountNumber = accountNumber, accountName = accountName };
            var client = await Utils.GetClient();
            var result = await client.PatchAsync(Url, new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<UpdateAccountNameResponse>(await result.Content.ReadAsStringAsync());
        }

        public async Task<CustomerAccount> GetCustomerAccountDetails(string customerId, string account)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + $"/details?accountNumber={account}&customerID={customerId}");
            var r = JsonConvert.DeserializeObject<CustomerAccount>(result);

            return r;
        }

        public async Task<GetBalanceResponse> GetBalance(string customerId, string account)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + $"/status?accountNumber={account}&customerID={customerId}");
            return JsonConvert.DeserializeObject<GetBalanceResponse>(result);
        }

        public async Task<AccountResponse> GetStatus(string customerId, string account)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + $"/status?accountNumber={account}&customerID={customerId}");
            return JsonConvert.DeserializeObject<AccountResponse>(result);
        }

        public async Task<AccountTransaction> GetAccountTransactions(string customerId, string account, DateTime from, DateTime to)
        {
            var client = await Utils.GetClient();
            string dateFrom = from.ToString("ddMMyyyy");
            string dateTo = to.ToString("ddMMyyyy");
            string result;
            try
            {
                HttpResponseMessage response = await client.GetAsync(Url + $"?accountNumber={account}&customerID={customerId}&dateFrom={dateFrom}&dateTo={dateTo}");

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AccountTransaction>(result);
                }                
            }
            catch (Exception)
            {
                return new AccountTransaction();
            }

            return null;
            
        }

        public async Task<List<Transaction>> GetCustomerAllAccountTransactions(string customerId, DateTime from, DateTime to)
        {
            var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "/customer/" + customerId);
            var accountCustomer = JsonConvert.DeserializeObject<AccountCustomer>(result);
            List<Transaction> accountTransactions = new List<Transaction>();

            foreach (var account in accountCustomer.accounts)
            {
                var details = await GetAccountTransactions(customerId, account.accountNumber.ToString(), from, to);
                if (details != null)
                {
                    accountTransactions.AddRange(details.transactions.ToList());
                }
            }
            return accountTransactions;
        }

        public async Task<AddDisponentRequestResponse> AddDisponent(string disponentId, string account)
        {
            var request = new AddDisponentRequestResponse { disponentCustomerID = disponentId, accountNumber = account };
            var client = await Utils.GetClient();
            var result = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<AddDisponentRequestResponse>(await result.Content.ReadAsStringAsync());
        }

    }

}
