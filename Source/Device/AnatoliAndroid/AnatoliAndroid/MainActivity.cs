﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Anatoli.App.Manager;
using Anatoli.Anatoliclient;
using Android.Net;
using Parse;

namespace AnatoliAndroid
{
    [Activity(Label = "AnatoliAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += async delegate
            {
                var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
                AnatoliClient.GetInstance(new AndroidWebClient(cn), new SQLiteAndroid(), new AndroidFileIO());
                //AnatoliUserManager um = new AnatoliUserManager();
                //var result = await um.RegisterAsync("a.toraby", "pass", "ALi Asghar", "Torabi", "09122073285");
                //button.Text = result.metaInfo.Result.ToString();
                ProductManager pm = new ProductManager();
                StoreManager sm = new StoreManager();
                try
                {
                    var list = await pm.GetFrequentlyOrderedProducts(2);
                    button.Text = list.Count.ToString();
                    var list2 = await sm.GetNearStores(4);
                    button.Text = list2.Count.ToString();
                    var list3 = await sm.GetAllAsync(0, 10, 2);
                    button.Text = list3.Count.ToString();
                }
                catch (Exception)
                {
                    throw;
                }
            };

        }
    }
}

