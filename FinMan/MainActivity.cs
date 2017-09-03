using Android.App;
using Android.Widget;
using Android.Graphics;
using Android.OS;
using FinMan.Data;
using System;
using System.Linq;
using FinMan.Custom;

namespace FinMan
{
    [Activity(Label = "FinMan", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private AccountClient accountClient;
        ListView listView;
        EditText customerNumberTextbox;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += OnGetAllAccountsClicked;
            accountClient = new AccountClient();

        }
        async void OnGetAllAccountsClicked(object sender, EventArgs e)
        {
            customerNumberTextbox = FindViewById<EditText>(Resource.Id.customerNumberTextBox);
            listView = FindViewById<ListView>(Resource.Id.accountListView);

            var accounts = await accountClient.GetAllAccountDetails(customerNumberTextbox.Text);
            var accountItems = accounts.Select(o => new AccountItem { AccountName = o.accountName, AccountNumber = o.accountNumber.ToString(), Balance = o.bookBalance, Color = o.accountType == "Current" ? Color.Blue : Color.Red, Currency = o.currency }).ToList<AccountItem>();

            listView.Adapter = new AccountAdapter(this, accountItems);
        }
    }
}

