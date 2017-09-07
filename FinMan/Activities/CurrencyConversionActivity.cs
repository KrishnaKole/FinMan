
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinManCore.Data;

namespace FinMan
{
    [Activity]
    public class CurrencyConversionActivity : Activity
    {
        string _userName;
        private const string _homeCurrency = "INR";
        private const string _baseCurrency = "NOK";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CurrencyConversion);

            _userName = Intent.Extras.GetString("Username", "");

            GetHomeCountryCurrencyRate(_baseCurrency);

            FindViewById<EditText>(Resource.Id.convertedAmountTextbox).TextChanged += GetConvertedCurrencyAmount;
        }

        private async void GetHomeCountryCurrencyRate(string baseCurrency)
        {
            CurrencyClient client = new CurrencyClient();

            var date = DateTime.Now.ToString("yyyy-mm-dd");
            var currencyRatesList = await client.GetExchangeRate(baseCurrency, date);

            var homeCurrencyRate = currencyRatesList.Where(a => a.CurrencyCode.Equals(_homeCurrency)).FirstOrDefault().CurrencyRate;
            FindViewById<EditText>(Resource.Id.currencyConversionTextbox).Text = "* The current curreny rate for 1 NOK is " + homeCurrencyRate + " INR";
        }

        private async void GetConvertedCurrencyAmount(object sender, EventArgs e)
        {
            CurrencyClient client = new CurrencyClient();

            var date = DateTime.Now.ToString("yyyy-mm-dd");
            var amount = FindViewById<EditText>(Resource.Id.amountTextbox).Text;
            var convertedAmount = await client.GetConversionAmount(_baseCurrency, _homeCurrency, amount, date);

            FindViewById<EditText>(Resource.Id.convertedAmountTextbox).Text = convertedAmount.Amount;
        }
    }
}
