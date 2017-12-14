using Android.App;
using Android.Widget;
using Android.OS;
using System;
using CoreApi;
using AndroidClient.Adapters;

namespace AndroidClient
{
    [Activity(Label = "AndroidClient", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btn = FindViewById<Button>(Resource.Id.MyBtn);
            var list = FindViewById<ListView>(Resource.Id.myList);
            btn.Click += delegate
            {
                Session session = new Session("test10", "123456");
                var adapter = new CustomerAdapter(session.GetAllCustomers().customers);
                list.Adapter = adapter;
            };
        }
    }
}