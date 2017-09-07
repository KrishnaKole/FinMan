using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinMan.Data;
using Newtonsoft.Json;

namespace FinManCore.Data
{
    public class CurrencyClient
    {
        public CurrencyClient()
        {
        }

		const string Url = "https://dnbapistore.com/hackathon/currencies/1.0/currency";
        public async Task<List<CurrencyExchange>> GetExchangeRate(string baseCurrency, string date)
		{
			var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "/exchange?base=" + baseCurrency + "&date=" + date);
			return JsonConvert.DeserializeObject<List<CurrencyExchange>>(result);
		}

        public async Task<AmountConversion> GetConversionAmount(string baseCurrency, string foreignCurrency, string amount, string date)
		{
			var client = await Utils.GetClient();
            string result = await client.GetStringAsync(Url + "/exchange?base=" + baseCurrency + "&foreignCurrency=" + foreignCurrency + "&amount=" + amount + "&date=" + date);
			return JsonConvert.DeserializeObject<AmountConversion>(result);
		}
	}
}
