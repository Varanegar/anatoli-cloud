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
using Anatoli.App.Model.AnatoliUser;
using Anatoli.App.Manager;
using Koush;
using System.Threading.Tasks;
using FortySevenDeg.SwipeListView;
using AnatoliAndroid.Activities;
using AnatoliAndroid.Fragments;
using Android.Content.Res;

namespace AnatoliAndroid.ListAdapters
{
    class ProductsListAdapter : BaseSwipeListAdapter<ProductManager, ProductModel>
    {
        TextView _productCountTextView;
        TextView _productNameTextView;
        TextView _productPriceTextView;
        TextView _bproductNameTextView;
        TextView _favoritsTextView;
        TextView _removeFromBasketTextView;

        ImageView _productIimageView;
        ImageButton _productAddButton;
        ImageView _bproductImageView;
        ImageButton _favoritsButton;
        ImageButton _productRemoveButton;
        ImageButton _removeAllProductsButton;
        OnTouchListener _addTouchlistener;
        LinearLayout _counterLinearLayout;
        RelativeLayout _removeAllRelativeLayout;
        RelativeLayout _relativeLayout4;
        LinearLayout _back;


        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {

            var view = (convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ProductSummaryLayout, null));

            ProductModel item = null;
            if (List != null)
                item = this[position];
            else
                return view;
            if (convertView == null)
            {
                _productNameTextView = view.FindViewById<TextView>(Resource.Id.productNameTextView);
                _favoritsTextView = view.FindViewById<TextView>(Resource.Id.favoritsTextView);
                _removeFromBasketTextView = view.FindViewById<TextView>(Resource.Id.removeFromBasketTextView);
                _bproductNameTextView = view.FindViewById<TextView>(Resource.Id.bproductNameTextView);
                _productPriceTextView = view.FindViewById<TextView>(Resource.Id.productPriceTextView);
                _productCountTextView = view.FindViewById<TextView>(Resource.Id.productCountTextView);
                _productIimageView = view.FindViewById<ImageView>(Resource.Id.productSummaryImageView);
                _bproductImageView = view.FindViewById<ImageView>(Resource.Id.bproductImageView);
                _productAddButton = view.FindViewById<ImageButton>(Resource.Id.addProductImageView);
                _productRemoveButton = view.FindViewById<ImageButton>(Resource.Id.removeProductImageView);
                _removeAllProductsButton = view.FindViewById<ImageButton>(Resource.Id.removeAllProductsButton);
                _removeAllRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.removeAllRelativeLayout);
                _favoritsButton = view.FindViewById<ImageButton>(Resource.Id.favoritsButton);
                _counterLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.counterLinearLayout);
                _relativeLayout4 = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayout4);
                _back = view.FindViewById<LinearLayout>(Resource.Id.back);

                view.SetTag(Resource.Id.productPriceTextView, _productPriceTextView);
                view.SetTag(Resource.Id.removeProductImageView, _productRemoveButton);
                view.SetTag(Resource.Id.favoritsTextView, _favoritsTextView);
                view.SetTag(Resource.Id.removeFromBasketTextView, _removeFromBasketTextView);
                view.SetTag(Resource.Id.addProductImageView, _productAddButton);
                view.SetTag(Resource.Id.productSummaryImageView, _productIimageView);
                view.SetTag(Resource.Id.bproductImageView, _bproductImageView);
                view.SetTag(Resource.Id.productCountTextView, _productCountTextView);
                view.SetTag(Resource.Id.productNameTextView, _productNameTextView);
                view.SetTag(Resource.Id.bproductNameTextView, _bproductNameTextView);
                view.SetTag(Resource.Id.removeAllProductsButton, _removeAllProductsButton);
                view.SetTag(Resource.Id.removeAllRelativeLayout, _removeAllRelativeLayout);
                view.SetTag(Resource.Id.favoritsButton, _favoritsButton);
                view.SetTag(Resource.Id.counterLinearLayout, _counterLinearLayout);
                view.SetTag(Resource.Id.relativeLayout4, _relativeLayout4);
                view.SetTag(Resource.Id.back, _back);
            }
            else
            {
                _productCountTextView = (TextView)view.GetTag(Resource.Id.productCountTextView);
                _favoritsTextView = (TextView)view.GetTag(Resource.Id.favoritsTextView);
                _removeFromBasketTextView = (TextView)view.GetTag(Resource.Id.removeFromBasketTextView);
                _productNameTextView = (TextView)view.GetTag(Resource.Id.productNameTextView);
                _bproductNameTextView = (TextView)view.GetTag(Resource.Id.bproductNameTextView);
                _productRemoveButton = (ImageButton)view.GetTag(Resource.Id.removeProductImageView);
                _productAddButton = (ImageButton)view.GetTag(Resource.Id.addProductImageView);
                _productIimageView = (ImageView)view.GetTag(Resource.Id.productSummaryImageView);
                _bproductImageView = (ImageView)view.GetTag(Resource.Id.bproductImageView);
                _productPriceTextView = (TextView)view.GetTag(Resource.Id.productPriceTextView);
                _removeAllProductsButton = (ImageButton)view.GetTag(Resource.Id.removeAllProductsButton);
                _removeAllRelativeLayout = (RelativeLayout)view.GetTag(Resource.Id.removeAllRelativeLayout);
                _favoritsButton = (ImageButton)view.GetTag(Resource.Id.favoritsButton);
                _counterLinearLayout = (LinearLayout)view.GetTag(Resource.Id.counterLinearLayout);
                _relativeLayout4 = (RelativeLayout)view.GetTag(Resource.Id.relativeLayout4);
                _back = (LinearLayout)view.GetTag(Resource.Id.back);
            }

            //if (!String.IsNullOrEmpty(item.image))
            //{
            //    UrlImageViewHelper.SetUrlDrawable(_productIimageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            //    UrlImageViewHelper.SetUrlDrawable(_bproductImageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            //}
            //else
            //{
            //    _productIimageView.SetImageResource(Resource.Drawable.igmart);
            //    _bproductImageView.SetImageResource(Resource.Drawable.igmart);
            //}

            string imguri = ProductManager.GetImageAddress(item.product_id, item.image);
            UrlImageViewHelper.SetUrlDrawable(_productIimageView, imguri, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            UrlImageViewHelper.SetUrlDrawable(_bproductImageView, imguri, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);


            if (item.IsFavorit)
            {
                _favoritsTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.RemoveFromList);
                _favoritsButton.SetImageResource(Resource.Drawable.ic_mylist_orange_24dp);
                _favoritsTextView.SetTextColor(Android.Graphics.Color.Red);
            }
            else
            {
                _favoritsTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.AddToList);
                _favoritsTextView.SetTextColor(Android.Graphics.Color.Green);
                _favoritsButton.SetImageResource(Resource.Drawable.ic_mylist_green_24dp);
            }

            _productCountTextView.Text = item.count.ToString() + " عدد";
            _productNameTextView.Text = item.product_name;
            _bproductNameTextView.Text = item.product_name;
            _productPriceTextView.Text = string.Format(" {0} تومان", item.price.ToCurrency());

            if (item.product_name.Equals(_productNameTextView.Text))
            {
                if (item.count > 0)
                {
                    _counterLinearLayout.Visibility = ViewStates.Visible;
                    _removeAllRelativeLayout.Visibility = ViewStates.Visible;
                }
                else
                {
                    _counterLinearLayout.Visibility = ViewStates.Gone;
                    _removeAllRelativeLayout.Visibility = ViewStates.Invisible;
                }
            }


            var removeAll = new OnTouchListener();
            _removeFromBasketTextView.SetOnTouchListener(removeAll);
            _removeAllProductsButton.SetOnTouchListener(removeAll);
            removeAll.Click += async (s, e) =>
            {
                //OnBackClicked(position);
                if (AnatoliApp.GetInstance().AnatoliUser != null)
                {
                    int a = await ShoppingCardManager.GetItemsCountAsync();
                    TextView counter = AnatoliApp.GetInstance().ShoppingCardItemCount;
                    double p = AnatoliApp.GetInstance().GetTotalPrice();
                    if (await ShoppingCardManager.RemoveProductAsync(item, true))
                    {
                        while (item.count > 0)
                        {
                            await Task.Delay(150);
                            item.count--;
                            counter.Text = (--a).ToString();
                            p = p - item.price;
                            AnatoliApp.GetInstance().SetTotalPrice(p);
                            NotifyDataSetChanged();
                            OnDataChanged();
                        }
                        if (item.product_name.Equals(_productNameTextView.Text))
                        {
                            _counterLinearLayout.Visibility = ViewStates.Gone;
                            _removeAllRelativeLayout.Visibility = ViewStates.Invisible;
                        }
                        NotifyDataSetChanged();
                        OnDataChanged();
                        OnShoppingCardItemRemoved(item);
                        Toast.MakeText(AnatoliApp.GetInstance().Activity, Resource.String.ItemRemoved, ToastLength.Short).Show();
                    }

                }
                else
                {
                    Toast.MakeText(AnatoliApp.GetInstance().Activity, Resource.String.PleaseLogin, ToastLength.Short).Show();
                    LoginFragment login = new LoginFragment();
                    var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                    login.Show(transaction, "login_dialog");
                }
            };



            var _favoritsTouchlistener = new OnTouchListener();
            _favoritsTextView.SetOnTouchListener(_favoritsTouchlistener);
            _favoritsButton.SetOnTouchListener(_favoritsTouchlistener);
            _favoritsTouchlistener.Click += async (s, e) =>
            {
                //OnBackClicked(position);
                if (this[position].IsFavorit)
                {
                    if (await ProductManager.RemoveFavorit(this[position].product_id.ToString()) == true)
                    {
                        this[position].favorit = 0;
                        NotifyDataSetChanged();
                        OnFavoritRemoved(this[position]);
                    }
                }
                else
                {
                    if (await ProductManager.AddToFavorits(this[position].product_id.ToString()) == true)
                    {
                        this[position].favorit = 1;
                        NotifyDataSetChanged();
                        OnFavoritAdded(this[position]);
                    }
                }

            };

            _addTouchlistener = new OnTouchListener();
            _productAddButton.SetOnTouchListener(_addTouchlistener);
            _addTouchlistener.Click += async (s, e) =>
            {
                if (AnatoliApp.GetInstance().AnatoliUser != null)
                {
                    if (await ShoppingCardManager.AddProductAsync(item))
                    {
                        item.count++;
                        if (item.product_name.Equals(_productNameTextView.Text))
                            if (item.count == 1)
                            {
                                _counterLinearLayout.Visibility = ViewStates.Visible;
                                _removeAllRelativeLayout.Visibility = ViewStates.Visible;
                            }
                        NotifyDataSetChanged();
                        OnDataChanged();
                        AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                        AnatoliAndroid.Activities.AnatoliApp.GetInstance().SetTotalPrice(await ShoppingCardManager.GetTotalPriceAsync());
                    }

                }
                else
                {
                    Toast.MakeText(AnatoliApp.GetInstance().Activity, Resource.String.PleaseLogin, ToastLength.Short).Show();
                    LoginFragment login = new LoginFragment();
                    var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                    login.Show(transaction, "login_dialog");
                }
            };

            var _removeTouchlistener = new OnTouchListener();
            _productRemoveButton.SetOnTouchListener(_removeTouchlistener);
            _removeTouchlistener.Click += async (s, e) =>
            {
                if (AnatoliApp.GetInstance().AnatoliUser != null)
                {
                    if (await ShoppingCardManager.RemoveProductAsync(item))
                    {
                        item.count--;
                        if (item.count == 0)
                        {
                            if (item.product_name.Equals(_productNameTextView.Text))
                            {
                                _counterLinearLayout.Visibility = ViewStates.Gone;
                                _removeAllRelativeLayout.Visibility = ViewStates.Invisible;
                            }
                            OnShoppingCardItemRemoved(item);
                        }
                        NotifyDataSetChanged();
                        OnDataChanged();
                        AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                        AnatoliAndroid.Activities.AnatoliApp.GetInstance().SetTotalPrice(await ShoppingCardManager.GetTotalPriceAsync());
                    }
                }
                else
                {
                    Toast.MakeText(AnatoliApp.GetInstance().Activity, Resource.String.PleaseLogin, ToastLength.Short).Show();
                    LoginFragment login = new LoginFragment();
                    var transaction = AnatoliApp.GetInstance().Activity.FragmentManager.BeginTransaction();
                    login.Show(transaction, "login_dialog");
                }
            };

            //_relativeLayout4.Click += (s, e) =>
            //{
            //    OnBackClicked(position);
            //};


            return view;
        }


        void OnShoppingCardItemRemoved(ProductModel data)
        {
            if (ShoppingCardItemRemoved != null)
            {
                ShoppingCardItemRemoved(this, data);
            }
        }
        public event ItemRemovedEventHandler ShoppingCardItemRemoved;
        public delegate void ItemRemovedEventHandler(object sender, ProductModel data);

        void OnShoppingCardItemAdded(ProductModel data)
        {
            if (ShoppingCardItemAdded != null)
            {
                ShoppingCardItemAdded(this, data);
            }
        }
        public event ItemAddedEventHandler ShoppingCardItemAdded;
        public delegate void ItemAddedEventHandler(object sender, ProductModel data);

        void OnFavoritRemoved(ProductModel data)
        {
            if (FavoritRemoved != null)
            {
                FavoritRemoved(this, data);
            }
        }
        public event FavoritRemovedEventHandler FavoritRemoved;
        public delegate void FavoritRemovedEventHandler(object sender, ProductModel data);

        void OnFavoritAdded(ProductModel data)
        {
            if (FavoritAdded != null)
            {
                FavoritAdded(this, data);
            }
        }
        public event FavoritAddedEventHandler FavoritAdded;
        public delegate void FavoritAddedEventHandler(object sender, ProductModel data);
    }
}