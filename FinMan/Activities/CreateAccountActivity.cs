
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
using FinMan.Data;
using Android.Support.V4.App;
using Android.Content.Res;
using Android.Support.V4.Widget;

namespace FinMan
{
    [Activity]
    public class CreateAccountActivity : Activity
    {
        protected DrawerLayout mDrawerLayout;
        protected List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;
        ActionBarDrawerToggle mDrawerTogle;
        Button nextPageButton;
        private string _userName;
        private string _personalNumber;
        private AccountClient accountClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateAccount);

            _userName = Intent.Extras.GetString("Username", "");
            _personalNumber = Intent.Extras.GetString("PersonalNumber", "");

            BindCurrencySpinner();

            accountClient = new AccountClient();

            Button buttonNext = FindViewById<Button>(Resource.Id.nextButton);
            buttonNext.Click += CreateNewAccount;
        }

        private void BindCurrencySpinner()
        {
            Spinner currencySpinner = FindViewById<Spinner>(Resource.Id.currencySpinner);

            var adapter1 = ArrayAdapter.CreateFromResource(this, Resource.Array.CurrencyArray, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            currencySpinner.Adapter = adapter1;
        }

        private void GoToCategoryActivity()
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(CategoryActivity));
            intent.PutExtra("Username", _userName);
            intent.PutExtra("PersonalNumber", _personalNumber);
            StartActivity(intent);
        }

        async void CreateNewAccount(object sender, EventArgs e)
        {
            var accountName = FindViewById<EditText>(Resource.Id.accountNameEditText).Text;
            var accountType = FindViewById<CheckBox>(Resource.Id.currentAccountCheckbox).Checked ? "Current" : (FindViewById<CheckBox>(Resource.Id.savingsAccountCheckbox).Checked ? "Savings" : "");
            var currencyCode = FindViewById<Spinner>(Resource.Id.currencySpinner).SelectedItem.ToString();

            AccountResponse newAccountResponse = await accountClient.CreateAccount(_personalNumber, accountName, accountType, currencyCode);

            GoToCategoryActivity();
        }
        private void SetNavigation()
        {
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftListView);

            mDrawerTogle = new ActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.nav_icon, Resource.String.open_drawer, Resource.String.close_drawer);
            mLeftItems.Add("Home");
            mLeftItems.Add("Notifications");
            mLeftItems.Add("Target History");
            mLeftItems.Add("Create DNB Account");
            mLeftItems.Add("Rename DNB Account");
            mLeftItems.Add("Send Money Home");
            mLeftItems.Add("Update Personal Details");
            mLeftItems.Add("Log Out");
            ArrayAdapter mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += (sender, e) =>
            {
                Intent intent = new Intent();
                string str = mLeftDrawer.GetItemAtPosition(e.Position).ToString();
                if (str == "Home")
                {

                    intent.SetClass(this, typeof(MainActivity));

                }
                if (str == "Notifications")
                {
                    //TODO                   

                }
                if (str == "Target History")
                {

                    //TODO  

                }
                if (str == "Create DNB Account")
                {

                    intent.SetClass(this, typeof(CreateAccountActivity));
                }
                if (str == "Rename DNB Account")
                {

                    //TODO
                }

                if (str == "Send Money Home")
                {

                    intent.SetClass(this, typeof(CurrencyConversionActivity));
                }
                if (str == "Update Personal Details")
                {
                    intent.SetClass(this, typeof(CreateCustomerActivity));
                    intent.PutExtra("CustomerUpdate", true);
                }
                intent.PutExtra("PersonalNumber", _personalNumber);
                StartActivity(intent);
            };

            mDrawerLayout.SetDrawerListener(mDrawerTogle);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
        }


    }
}
