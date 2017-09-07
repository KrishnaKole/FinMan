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
using static FinMan.Resource;

namespace FinMan.Custom
{
    public class AccountAdapter : BaseAdapter<AccountItem>
    {
        List<AccountItem> items;
        Activity context;
        public AccountAdapter(Activity context, List<AccountItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override AccountItem this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItem, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = item.AccountName + " - " + item.AccountNumber;
            view.FindViewById<TextView>(Resource.Id.textView2).Text = item.Currency + " " + item.Balance.ToString();
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetBackgroundColor(item.Color);

            return view;
        }
    }
    public class AccountItem
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public Android.Graphics.Color Color { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
    }

    public class Settings
    {
        public static string CustomerNumber { get; set; }
        public static List<string> AccountList { get; set; }
        public static List<Target> TargetList { get; set; }
    }


}