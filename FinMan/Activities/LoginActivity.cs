using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

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