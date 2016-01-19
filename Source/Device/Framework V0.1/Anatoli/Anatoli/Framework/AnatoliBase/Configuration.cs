﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public class Configuration
    {
        public static readonly string parseAppId = "wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI";
        public static readonly string parseDotNetKey = "G7guVuyx35bb4fBOwo7BVhlG2L2E2qKLQI0sLAe0";
        public static readonly string userInfoFile = "userInfo";
        public static readonly string customerInfoFile = "customerInfo";
        public static readonly string tokenInfoFile = "tk.info";
        public struct AppMobileAppInfo
        {
            public static readonly string UserName = "AnatoliMobileApp";
            public static readonly string Password = "Anatoli@App@Vn";
            //public static readonly string UserName = "petropay";
            //public static readonly string Password = "petropay";
            public static readonly string Scope = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        }
        public struct WebService
        {
            public static readonly string PortalAddress = "http://79.175.166.186/";
            public static readonly string OAuthTokenUrl = "/oauth/token";
            public static readonly string Images = "/api/imageManager/images/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            public static readonly string ParseInfo = "api/TestAuth/setParsInfo";
            public struct Products
            {
                public static readonly string ProductView = "product_view.php";
                public static readonly string ProductsView = "/api/gateway/product/products/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string FOGList = "list.php";
                public static readonly string RateProductUri = "/Products/Rate";
                public static readonly string ProductGroups = "/api/gateway/product/productgroups/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string FavoritsView = "/Products/Rate";
            }
            public struct Stores
            {
                public static readonly string StoreView = "store_view";
                public static readonly string StoresView = "/api/gateway/store/stores/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string PricesView = "/api/gateway/store/storepricelist/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string CityRegion = "/api/gateway/base/region/cityregions/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                public static readonly string DeliveryTime = "/api/gateway/store/storecalendar/?Id=23fc03ff-53f0-432c-b6f4-e560b9088d54&PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            }
            public struct Users
            {
                public static readonly string UserCreateUrl = "/api/accounts/create";
                public static readonly string UserAuthUrl = "/api/accounts/create";
                public static readonly string ViewProfileUrl = "/api/gateway/customer/customers";
                public static readonly string SaveProfileUrl = "/api/gateway/customer/save";
                public static readonly string ShoppingCard = "";
                public static readonly string OrdersHistory = "api/gateway/purchaseorder/history";
            }
            public static readonly string CityRegion = "/api/gateway/base/region/cityregions/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
            public static readonly string CityRegionAfter = "/api/gateway/base/region/cityregions/after/?PrivateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        }
    }
}
