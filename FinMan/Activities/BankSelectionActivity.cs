
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
using Xamarin.Forms.PlatformConfiguration;

namespace FinMan
{
    [Activity]
    public class BankSelectionActivity : Activity
    {
        string _userName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BankSelection);

            _userName = Intent.Extras.GetString("Username", "");

            BindCountrySpinner();

            Button buttonNext = FindViewById<Button>(Resource.Id.nextButton);
            buttonNext.Click += NavigateToAccountDetailsActivity;

            TextView textViewNewCustomer = FindViewById<TextView>(Resource.Id.newCustomerButton);
            textViewNewCustomer.Click += NavigateToCreateNewCustomerActivity;
        }

        private void BindCountrySpinner()
        {
            Spinner countrySpinner = FindViewById<Spinner>(Resource.Id.countrySpinner);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.CountryArray, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            countrySpinner.Adapter = adapter;
        }

        private void NavigateToAccountDetailsActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(MainActivity));
            //var username = FindViewById<EditText>(Resource.Id.usernameEditText).Text;
            intent.PutExtra("Username", _userName);
            intent.PutExtra("PersonalNumber", FindViewById<EditText>(Resource.Id.personalNumberTextbox).Text);
            StartActivity(intent);
        }

        private void NavigateToCreateNewCustomerActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(CreateCustomerActivity));
            intent.PutExtra("Username", _userName);
            intent.PutExtra("PersonalNumber", FindViewById<EditText>(Resource.Id.personalNumberTextbox).Text);
            StartActivity(intent);
        }
    }
}
