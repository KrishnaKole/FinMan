using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;


namespace FinMan.Data
{
    class PaymentClient
    {
        const string Url = "https://dnbapistore.com/hackathon/payments/1.0/payment";

        public async Task<InitiatePaymentResponse> InitiatePayment(string debitAccount, string creditAccount,string message, double amount, DateTime date)
        {
            InitiatePaymentRequest request = new InitiatePaymentRequest { debitAccountNumber = debitAccount, creditAccountNumber = creditAccount, message = message, amount = amount.ToString(), paymentDate = date.ToString("yyyy-mm-dd") };
            var client = await Utils.GetClient();
            var result = await client.PutAsync(Url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<InitiatePaymentResponse>(await result.Content.ReadAsStringAsync());
        }
        public async Task<InitiatePaymentWithKidResponse> InitiatePaymentWithKid(string debitAccount, string creditAccount, string kid, double amount, DateTime date)
        {
            InitiatePaymentWithKidRequest request = new InitiatePaymentWithKidRequest { debitAccountNumber = debitAccount, creditAccountNumber = creditAccount, kidNumber = kid, amount = amount.ToString(), paymentDate = date.ToString("yyyy-mm-dd") };
            var client = await Utils.GetClient();
            var result = await client.PutAsync(Url, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<InitiatePaymentWithKidResponse>(await result.Content.ReadAsStringAsync());
        }


    }
}
