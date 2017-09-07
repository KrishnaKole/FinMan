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
using Android.Support.V4.Widget;
using FinMan;
using Android.Support.V4.App;
using Android.Content.Res;


namespace FinMan
{
    [Activity(Label = "MenuNavActivity", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MenuNavActivity : Activity
    {
        protected DrawerLayout mDrawerLayout;
        protected List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;
        ActionBarDrawerToggle mDrawerTogle;
        Button nextPageButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuNav);
        }
        private void SetNavigation()
        {
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.leftListView);

            mDrawerTogle = new ActionBarDrawerToggle(this, mDrawerLayout, Resource.Drawable.nav_icon, Resource.String.open_drawer, Resource.String.close_drawer);
            mLeftItems.Add("Home");
            mLeftItems.Add("UpdatePersonalDetails");
            mLeftItems.Add("Notification");
            mLeftItems.Add("Send Money Home");
            mLeftItems.Add("Target History");
            mLeftItems.Add("LogOut");
            ArrayAdapter mLeftAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += (sender, e) =>
            {

                string str = mLeftDrawer.GetItemAtPosition(e.Position).ToString();
                if (str == "Home")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                if (str == "UpdatePersonalDetails")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(LoginActivity));
                    StartActivity(intent);
                }
                if (str == "Notification")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                if (str == "Notification")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CategoryActivity));
                    StartActivity(intent);
                }
                if (str == "Notification")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CategoryActivity));
                    StartActivity(intent);
                }
                if (str == "Notification")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CategoryActivity));
                    StartActivity(intent);
                }

            };


            mDrawerLayout.SetDrawerListener(mDrawerTogle);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
        }
        private void OnNextPage(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(MainActivity));
            StartActivity(intent);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerTogle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerTogle.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerTogle.OnOptionsItemSelected(item))
            {

                return true;
            }
            return base.OnOptionsItemSelected(item);
        }


    }
}