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
using CoreApi;

namespace AndroidClient.Adapters
{
    class CustomerAdapter : BaseAdapter<Customer>
    {
        protected IList<Customer> _customers;
        //Context context;

        public CustomerAdapter(IList<Customer> customers)
        {
            _customers = customers;
        }
        public override Customer this[int position] => _customers[position];


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CustomerView, parent, false);

            var name = view.FindViewById<TextView>(Resource.Id.customer_name_text);
            var bday = view.FindViewById<TextView>(Resource.Id.customer_bday_text);

            name.Text = _customers[position].first_name;
            bday.Text = _customers[position].Birth_day.ToString();
            return view;
        }

        public override int Count
        {
            get
            {
                return _customers.Count;
            }
        }

    }
}