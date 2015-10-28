using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Store;
using Anatoli.Framework.Model;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    class StoresListFragment : BaseListFragment<StoreManager, StoresListAdapter, StoreDataModel>
    {
        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            return parameters;
        }

        protected override string GetTableName()
        {
            return "stores";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Stores.StoresView;
        }
    }
}