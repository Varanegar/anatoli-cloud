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
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.Model;
using System.Threading.Tasks;
using AnatoliAndroid.Activities;
using AnatoliAndroid.Components;
using Android.Views.Animations;

namespace AnatoliAndroid.Fragments
{
    abstract class BaseSwipeListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel> : BaseListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel>
        where BaseDataManager : BaseManager<DataModel>, new()
        where DataListAdapter : BaseSwipeListAdapter<BaseDataManager, DataModel>, new()
        where ListTools : ListToolsDialog, new()
        where DataModel : BaseDataModel, new()
    {
        protected override View InflateLayout(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(
                Resource.Layout.ItemsSwipeListLayout, container, false);
            return view;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _listAdapter.BackClick += async (s, p) =>
            {
                await Task.Delay(1000);
                try
                {
                    //var sl = _listView as SwipeListView;
                    //sl.CloseOpenedItems();
                }
                catch (Exception)
                {

                }
            };
            _listView.ScrollStateChanged += _listView_ScrollStateChanged;
            return view;
        }

        void _listView_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            try
            {
                //var sl = _listView as SwipeListView;
                //sl.CloseOpenedItems();
                for (int i = _listView.FirstVisiblePosition; i < _listView.LastVisiblePosition; i++)
                {
                    try
                    {
                        int childPosition = i - _listView.FirstVisiblePosition;
                        var optionsLinearLayout = _listView.GetChildAt(childPosition).FindViewById<LinearLayout>(Resource.Id.optionslinearLayout);
                        if (optionsLinearLayout.Visibility == ViewStates.Visible)
                        {
                            Animation anim = AnimationUtils.LoadAnimation(AnatoliApp.GetInstance().Activity, Resource.Animation.slideOut);
                            optionsLinearLayout.StartAnimation(anim);
                            optionsLinearLayout.Visibility = ViewStates.Gone;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }



}