using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot.Xamarin.Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using FinMan.Data;
using System.Threading.Tasks;
using NChart3D_Android;
using DifferentCharts;

namespace FinMan
{
    [Activity(Label = "CategoryActivity")]
    public class CategoryActivity : Activity
    {
        NChartView mNChartView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Category);
            // Create your application here
            var customerId = Intent.Extras.GetString("CustomerId", "14127497026");
            customerId = customerId == "" ? "14127497026" : customerId;
            //PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            //view.Model = await CreatePlotModel(customerId);

            NChartView view = FindViewById<NChartView>(Resource.Id.nchart3d);
           // await CreatePlotModel(customerId);

        }
        private async void CreatePlotModel(string customerId)
        {
            AccountClient client = new AccountClient();
            var transactions = await client.GetCustomerAllAccountTransactions(customerId, DateTime.Now.AddMonths(-6), DateTime.Now);

            var categorizedResult = CategoryManager.CategorizeTransactions(transactions);

            var plotModel = new PlotModel { Title = "My Finance Profile" };

            mNChartView = FindViewById<NChartView>(Resource.Id.nchart3d);
            mNChartView.Chart.LicenseKey = "mCYQtBS4P+HjVNrB3+y2x6lxzg2+JfwJWd0Nw43yK5ZuDt9OZ858Phq7Iizim14QxQVELJVeGJvOc/IROPQQkIdoISwhGrlA+GO+sk7sPXSha7XhKO46WJPdMMBtjBy+N7y02WrTn7YXibEbKegmZpC6Cnac+eOYhhXTOgQk39GEBaISLEi3edAQ3X5KpdfP01sYAOMEiN3vhfdNuYOrzdq7J9DishKMDDVaKWmjKEvq+i4HGQMv4HcXZagc0wKgYqNY3JiwLZrno0SmVEK9g3UWQpLNVzkoYzjGwJq4NsrAV79BSEhKuhia94JcT97w3DUooalMpdhjALem199K9vAr/fMP9DnLb286aDF2sl5W65kn4LlbIRIdnWM/C8ugw86NbamAo0AOk564ZI01GNJxZ8cYYo5uLM1/OuvDsiy4gddHxfrgEpbrWqFN9jrQvQ7oxHKh1k5sSP9BmE62B0luB7I6tetmaDcL7ALUU3jKuvHt4+kkyVTQ77GZbWKSw12i6esiEeDSq8VWVdE1NwDSYxsQop2ycAAhZgNqcegcFI/QVBpxpfVTZFx3dubT86j52fw+QOXUiNc4zv3xh2bmetP0wX7dxHLi8v4YCNYlQZrWVA84WG43b2Wc2zJV1A6RmmVOl5i0BKJjAwP38W567Myav3XOm90bInQDDB0=";

            //dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            //foreach (var category in categorizedResult)
            //{
            //    seriesP1.Slices.Add(new PieSlice(category.Key.ToString(), category.Value) { IsExploded = true });
            //}

            //plotModel.Series.Add(seriesP1);

            //return plotModel;

            PieChartController controller = new PieChartController(mNChartView);
            controller.DrawIn3D = true;
            controller.HoleRatio = 0.7f;
            controller.UpdateData();
        }
    }
}