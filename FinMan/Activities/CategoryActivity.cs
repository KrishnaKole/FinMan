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

namespace FinMan
{
    [Activity]
    public class CategoryActivity : Activity
    {
        NChartView mNChartView;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Category);
            // Create your application here
            var customerId = Intent.Extras.GetString("CustomerId", "14127497026");
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
            pb.Progress = (int)((-1 * target.SpentAmount) * 100 / target.AllowedAmount);

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
            var prev = DateTime.Now.AddMonths(-6);
            var transactions = await client.GetCustomerAllAccountTransactions(customerId, new DateTime(prev.Year, prev.Month, 1), new DateTime(prev.Year, prev.AddMonths(5).Month,
                                     DateTime.DaysInMonth(prev.Year, prev.AddMonths(5).Month)));

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
    }
}