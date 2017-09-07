using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FinMan.Resources;
using Java.Lang;
using Newtonsoft.Json;

namespace FinMan
{
    [Activity]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            Button buttonLogIn = FindViewById<Button>(Resource.Id.logInButton);
            buttonLogIn.Click += NavigateToBankSelectionActivity;
        }

        private void NavigateToBankSelectionActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(BankSelectionActivity));
            var username = FindViewById<EditText>(Resource.Id.usernameEditText).Text;
            intent.PutExtra("Username", username);
            StartActivity(intent);
        }
    }
}