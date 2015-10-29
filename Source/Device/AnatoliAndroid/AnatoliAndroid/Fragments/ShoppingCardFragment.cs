﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Product;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.App.Model;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    class ShoppingCardFragment : Fragment
    {
        ListView _itemsListView;
        TextView _deliveryAddress;
        TextView _factorPrice;
        EditText _delivaryDate;
        EditText _deliveryTime;
        ProductsListAdapter _listAdapter;
        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ShoppingCardLayout, container, false);
            _itemsListView = view.FindViewById<ListView>(Resource.Id.shoppingCardListView);
            _deliveryAddress = view.FindViewById<TextView>(Resource.Id.addressTextView);
            _factorPrice = view.FindViewById<TextView>(Resource.Id.factorPriceTextView);
            _delivaryDate = view.FindViewById<EditText>(Resource.Id.deliveryDateEditText);
            _deliveryTime = view.FindViewById<EditText>(Resource.Id.deliveryTimeEditText);
            _factorPrice.Text = ShoppingCardManager.GetTotalPrice().ToString();
            _listAdapter = new ProductsListAdapter();
            _listAdapter.List = ShoppingCardManager.GetAllItems();
            _listAdapter.NotifyDataSetChanged();
            _listAdapter.DataRemoved += (s, item) =>
            {
                _listAdapter.List.Remove(item);
                _itemsListView.InvalidateViews();
            };
            _itemsListView.Adapter = _listAdapter;
            if (_listAdapter.Count == 0)
            {
                Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "سبد خرید خالی است", ToastLength.Short).Show();
            }
            return view;
        }

    }
}