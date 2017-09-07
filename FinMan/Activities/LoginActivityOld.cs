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
using Java.Lang;
using Newtonsoft.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace FinMan
{
    [Activity(Label = "Login", Icon = "@mipmap/icon"/*, MainLauncher = true*/)]
    public class LoginActivityOld : Activity, IFacebookCallback
    {
        private ICallbackManager mCallbackManager;
        private ProfilePictureView mProfilePic;
        private MyProfileTracker mProfileTracker;
        private TextView txtFistName;
        private TextView txtLastName;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FacebookSdk.SdkInitialize(this.ApplicationContext);
            // Create your application here
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            mProfileTracker = new MyProfileTracker();
            mProfileTracker.StartTracking();
            mProfileTracker.mOnProfileChanged += MProfileTracker_mOnProfileChanged;

            //txtFistName = FindViewById<TextView>(Resource.Id.FirstName);
            //txtLastName = FindViewById<TextView>(Resource.Id.LastName);
            mProfilePic = FindViewById<ProfilePictureView>(Resource.Id.ProfilePic);

            // Get our button from the layout resource,
            // and attach an event to it
            if (AccessToken.CurrentAccessToken != null)
            {

            }

            LoginButton button = FindViewById<LoginButton>(Resource.Id.FacebookButton);
            button.SetReadPermissions(new List<string> { "user_friends", "public_profile" });
            mCallbackManager = CallbackManagerFactory.Create();
            button.RegisterCallback(mCallbackManager, this);

        }

        private void MProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                txtFistName.Text = e.mProfile.FirstName;
                txtLastName.Text = e.mProfile.LastName;
                mProfilePic.ProfileId = e.mProfile.Id;
            }

        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnError(FacebookException p0)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            LoginResult loginResult = result as LoginResult;
            Console.WriteLine(loginResult.AccessToken.UserId);
        }

        protected override void OnDestroy()
        {
            mProfileTracker.StopTracking();
            base.OnDestroy();
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }

    public class MyProfileTracker : ProfileTracker
    {
        public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;
        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
        {
            if (mOnProfileChanged != null)
            {
                mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
            }
        }
    }
    public class OnProfileChangedEventArgs : EventArgs
    {
        public Profile mProfile;
        public OnProfileChangedEventArgs(Profile profile)
        {
            mProfile = profile;
        }
    }
}