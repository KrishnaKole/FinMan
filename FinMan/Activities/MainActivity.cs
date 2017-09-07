using Android.App;
using Android.Widget;
using Android.Graphics;
using Android.OS;
using FinMan.Data;
using System;
using System.Linq;
using FinMan.Custom;
using Android.Content;

namespace FinMan
{
    [Activity (MainLauncher = false)]
    public class MainActivity : Activity
    {
        int count = 1;
        private AccountClient accountClient;
        ListView listView;

        string _personalNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            

            accountClient = new AccountClient();
            _personalNumber = "14127497026";// Intent.Extras.GetString("PersonalNumber", "14127497026");
            OnGetAllAccountsClicked();
            Button profileButton = FindViewById<Button>(Resource.Id.goToProfile);
            profileButton.Click += OnGoToProfileClicked;

        }

        private void OnGoToProfileClicked(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(CategoryActivity));
            intent.PutExtra("PersonalNumber", _personalNumber);
            StartActivity(intent);
        }

        async void OnGetAllAccountsClicked()
        {
            var accounts = await accountClient.GetAllAccountDetails("14127497026");
            var accountItems = accounts.Select(o => new AccountItem { AccountName = o.accountName, AccountNumber = o.accountNumber.ToString(), Balance = o.bookBalance, Color = o.accountType == "Current" ? Color.Blue : Color.Red, Currency = o.currency }).ToList<AccountItem>();

            listView.Adapter = new AccountAdapter(this, accountItems);
        }


    }
}

