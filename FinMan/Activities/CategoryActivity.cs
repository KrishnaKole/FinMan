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
using System.Threading.Tasks;
using NChart3D_Android;
using DifferentCharts;
using Android.Support.V4.Widget;
using FinMan;
using Android.Support.V4.App;
using Android.Content.Res;


namespace FinMan
{
    [Activity]
    public class CategoryActivity : Activity
    {
        protected DrawerLayout mDrawerLayout;
        protected List<string> mLeftItems = new List<string>();
        ArrayAdapter mLeftAdapter;
        ListView mLeftDrawer;
        ActionBarDrawerToggle mDrawerTogle;
        Button nextPageButton;
        NChartView mNChartView;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Category);
            // Create your application here
            SetNavigation();
            var customerId = Intent.Extras.GetString("PersonalNumber", "14127497026");
            customerId = customerId == "" ? "14127497026" : customerId;
            ProgressBar pb = FindViewById<ProgressBar>(Resource.Id.targetProgressBar);
            pb.Elevation = 20;
            pb.Tag = "Target";
            pb.LayoutParameters.Height = 50;


            NChartView view = FindViewById<NChartView>(Resource.Id.nchart3d);
            mNChartView = FindViewById<NChartView>(Resource.Id.nchart3d);
            mNChartView.Chart.LicenseKey = "mCYQtBS4P+HjVNrB3+y2x6lxzg2+JfwJWd0Nw43yK5ZuDt9OZ858Phq7Iizim14QxQVELJVeGJvOc/IROPQQkIdoISwhGrlA+GO+sk7sPXSha7XhKO46WJPdMMBtjBy+N7y02WrTn7YXibEbKegmZpC6Cnac+eOYhhXTOgQk39GEBaISLEi3edAQ3X5KpdfP01sYAOMEiN3vhfdNuYOrzdq7J9DishKMDDVaKWmjKEvq+i4HGQMv4HcXZagc0wKgYqNY3JiwLZrno0SmVEK9g3UWQpLNVzkoYzjGwJq4NsrAV79BSEhKuhia94JcT97w3DUooalMpdhjALem199K9vAr/fMP9DnLb286aDF2sl5W65kn4LlbIRIdnWM/C8ugw86NbamAo0AOk564ZI01GNJxZ8cYYo5uLM1/OuvDsiy4gddHxfrgEpbrWqFN9jrQvQ7oxHKh1k5sSP9BmE62B0luB7I6tetmaDcL7ALUU3jKuvHt4+kkyVTQ77GZbWKSw12i6esiEeDSq8VWVdE1NwDSYxsQop2ycAAhZgNqcegcFI/QVBpxpfVTZFx3dubT86j52fw+QOXUiNc4zv3xh2bmetP0wX7dxHLi8v4YCNYlQZrWVA84WG43b2Wc2zJV1A6RmmVOl5i0BKJjAwP38W567Myav3XOm90bInQDDB0=";
            CreatePlotModel(customerId);

            var target = await GetTarget(customerId);
            pb.Progress = (int)((target.SpentAmount) * 100 / target.AllowedAmount);

            TextView spent = FindViewById<TextView>(Resource.Id.spentText);
            spent.SetTextColor(Android.Graphics.Color.Red);
            TextView targetCategory = FindViewById<TextView>(Resource.Id.myTargetCategoryText);
            targetCategory.SetTextColor(Android.Graphics.Color.Yellow);
            TextView allowed = FindViewById<TextView>(Resource.Id.allowedText);
            allowed.SetTextColor(Android.Graphics.Color.Green);
            spent.Text = "Spent : " + target.SpentAmount.ToString("0");
            targetCategory.Text = target.Category.ToString();
            allowed.Text = "Allowed : " + target.AllowedAmount.ToString("0");


        }

        private async Task<Target> GetTarget(string customerId)
        {
            AccountClient client = new AccountClient();
            var prev = DateTime.Now.AddMonths(-4);
            var transactions = await client.GetCustomerAllAccountTransactions(customerId, new DateTime(prev.Year, prev.Month, 1), new DateTime(prev.Year, prev.AddMonths(3).Month,
                                     DateTime.DaysInMonth(prev.Year, prev.AddMonths(3).Month)));

            var categorizedResult = CategoryManager.CategorizeTransactions(transactions);

            var now = DateTime.Now;
            var currentTransactions = await client.GetCustomerAllAccountTransactions(customerId, new DateTime(now.Year, now.Month, 1), new DateTime(now.Year, now.Month,
                                     DateTime.DaysInMonth(now.Year, now.Month)));

            var currentCategorizedResult = CategoryManager.CategorizeTransactions(currentTransactions);

            var target = TargetGenerator.GetCurrentTarget(categorizedResult, currentCategorizedResult);
            return target;
        }



        private async void CreatePlotModel(string customerId)
        {
            AccountClient client = new AccountClient();
            var transactions = await client.GetCustomerAllAccountTransactions(customerId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now);

            var categorizedResult = CategoryManager.CategorizeTransactions(transactions);
            PieChartController controller = new PieChartController(mNChartView);
            controller.DrawIn3D = true;
            controller.HoleRatio = 0.6f;
            controller.UpdateData(categorizedResult);
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
                    intent.SetClass(this, typeof(CreateCustomerActivity));
                    StartActivity(intent);
                }
                if (str == "My Target")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CategoryActivity));
                    StartActivity(intent);
                }
                if (str == "Create Account")
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(CreateAccountActivity));
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