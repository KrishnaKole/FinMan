using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Java.Lang;
using System.Collections.Generic;

using NChart3D_Android;
using FinMan.Data;
using Android.Graphics.Drawables;

namespace DifferentCharts
{
    public class PieChartController : Java.Lang.Object, INChartSeriesDataSource
    {

        NChartView mNChartView;
        Random random = new Random();

        public bool DrawIn3D { get; set; }

        public float HoleRatio { get; set; }

        Dictionary<Category, NChartBrush> brushes;
        Dictionary<Category, double> CategoryTrans;
        public PieChartController(NChartView view)
        {
            mNChartView = view;

        }

        public void UpdateData(Dictionary<Category, double> category)
        {
            CategoryTrans = category;
            // Switch on antialiasing.
            mNChartView.Chart.ShouldAntialias = true;
            mNChartView.Chart.Background = new NChartLinearGradientBrush(Color.Argb(255, 160, 160, 160), Color.BurlyWood);
            mNChartView.Chart.Caption.Text = "My Expenses";
            mNChartView.Chart.Legend.BlockAlignment = NChartLegendBlockAlignment.TopLeft;
            mNChartView.Chart.Legend.ContentAlignment = NChartLegendContentAlignment.Left;


            if (DrawIn3D)
            {
                // Switch 3D on.
                mNChartView.Chart.DrawIn3D = true;
                mNChartView.Chart.CartesianSystem.Margin = new NChartMargin(10.0f, 10.0f, 10.0f, 10.0f);
                mNChartView.Chart.PolarSystem.Margin = new NChartMargin(10.0f, 10.0f, 10.0f, 10.0f);
            }
            else
            {
                mNChartView.Chart.CartesianSystem.Margin = new NChartMargin(10.0f, 10.0f, 10.0f, 20.0f);
                mNChartView.Chart.PolarSystem.Margin = new NChartMargin(10.0f, 10.0f, 10.0f, 20.0f);
            }

            // Create series that will be displayed on the chart.
            CreateSeries(category);

            // Update data in the chart.
            mNChartView.Chart.UpdateData();
        }

        void CreateSeries(Dictionary<Category, double> category)
        {
            brushes = new Dictionary<Category, NChartBrush>();
            Category temp = Category.Transport;
            if (category.ContainsKey(temp))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 138, 43, 226));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }
            temp = Category.Home;
            if (category.ContainsKey(temp))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 0, 100, 0));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }
            temp = Category.Shopping;
            if (category.ContainsKey(temp))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 255, 228, 181));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }

            temp = Category.Food;
            if (category.ContainsKey(temp))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 255, 0, 0));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }

            temp = Category.CommunicationEntertainment;
            if (category.ContainsKey(temp))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 255, 255, 0));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }

            temp = Category.Overall;
            if (category.ContainsKey(Category.Overall))
            {
                brushes[temp] = new NChartSolidColorBrush(Color.Argb(255, 0, 0, 255));
                NChartPieSeries series = GetSeries(temp);
                mNChartView.Chart.AddSeries(series);
            }

            NChartPieSeriesSettings settings = new NChartPieSeriesSettings();
            settings.HoleRatio = HoleRatio;
            mNChartView.Chart.AddSeriesSettings(settings);
        }

        private NChartPieSeries GetSeries(Category cat)
        {
            NChartPieSeries series = new NChartPieSeries();
            series.DataSource = this;
            series.Tag = (int)cat;
            series.Brush = brushes[cat];
            return series;
        }

        public NChartPoint[] Points(NChartSeries series)
        {
            // Create points with some data for the series.
            List<NChartPoint> result = new List<NChartPoint>();

            result.Add(new NChartPoint(GetPoints(series.Tag), series));

            return result.Count > 0 ? result.ToArray() : null;
        }

        private NChartPointState[] GetPoints(int tag)
        {
            NChartPointState[] a = new NChartPointState[CategoryTrans.Count];
            int i = 0;
            if (CategoryTrans.ContainsKey((Category)tag))
            {
                a[i++] = NChartPointState.PointStateWithCircleValue(0, -1 * CategoryTrans[(Category)tag]);
            }

            return a;
        }

        public NChartPoint[] ExtraPoints(NChartSeries series)
        {
            return null;
        }

        public string Name(NChartSeries series)
        {
            return string.Format("{0} {1}", (Category)series.Tag, (long)CategoryTrans[(Category)series.Tag]);
        }

        public Bitmap Image(NChartSeries series)
        {
            return null;
        }
    }
}


