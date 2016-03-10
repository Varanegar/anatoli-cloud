﻿using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Product;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model.Store;
using Anatoli.App.Model;

namespace Anatoli.App.Manager
{
    public class ShoppingCardManager : BaseManager<ProductModel>
    {
        public static async Task<ProductModel> GetItemAsync(string id)
        {
            try
            {
                SelectQuery query = new SelectQuery("shopping_card_view", new EqFilterParam("product_id", id.ToString().ToUpper()));
                return await BaseDataAdapter<ProductModel>.GetItemAsync(query);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<bool> AddProductAsync(string productId, int count)
        {
            DBQuery query = null;
            try
            {
                var item = await GetItemAsync(productId);
                if (item == null || item.count == 0)
                    query = new InsertCommand("shopping_card", new BasicParam("count", (count).ToString()), new BasicParam("product_id", productId));
                else
                    query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count + count).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
                return await DataAdapter.UpdateItemAsync(query) > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<bool> AddProductAsync(ProductModel item)
        {
            try
            {
                DBQuery query = null;
                if (item.count == 0)
                    query = new InsertCommand("shopping_card", new BasicParam("count", (item.count + 1).ToString()), new BasicParam("product_id", item.product_id.ToString()));
                else
                    query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count + 1).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
                return await DataAdapter.UpdateItemAsync(query) > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<bool> RemoveProductAsync(ProductModel item, bool all = false)
        {
            try
            {
                DBQuery query = null;
                if (item.count <= 1 || all)
                    query = new DeleteCommand("shopping_card", new SearchFilterParam("product_id", item.product_id.ToString()));
                else
                    query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count - 1).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
                return await DataAdapter.UpdateItemAsync(query) > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async static Task<double> GetTotalPriceAsync()
        {
            try
            {
                SelectQuery query = new SelectQuery("shopping_card_view");
                query.Unlimited = true;
                var result = await BaseDataAdapter<ProductModel>.GetListAsync(query);
                double p = 0;
                foreach (var item in result)
                {
                    p += (item.count * item.price);
                }
                return p;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static async Task<List<ProductModel>> GetAllItemsAsync()
        {
            try
            {
                var defaultStore = await StoreManager.GetDefaultAsync();
                StringQuery query = new StringQuery(string.Format("SELECT * FROM shopping_card_view LEFT JOIN store_onhand ON shopping_card_view.product_id = store_onhand.product_id WHERE store_onhand.store_id = '{0}'", defaultStore.store_id));
                query.Unlimited = true;
                var list = await BaseDataAdapter<ProductModel>.GetListAsync(query);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<bool> ClearAsync()
        {
            DeleteCommand command = new DeleteCommand("shopping_card");
            return (await DataAdapter.UpdateItemAsync(command) > 0) ? true : false;
        }

        public static async Task<int> GetItemsCountAsync()
        {
            var list = await GetAllItemsAsync();
            if (list != null)
            {
                int count = 0;
                foreach (var item in list)
                {
                    count += item.count;
                }
                return count;
            }
            else
                return 0;
        }

        public static async Task<bool> UpdateProductCountAsyc(ProductModel item)
        {
            DBQuery query = null;
            if (item.count == 0)
                query = new DeleteCommand("shopping_card", new SearchFilterParam("product_id", item.product_id.ToString()));
            else
                query = new UpdateCommand("shopping_card", new BasicParam("count", (item.count).ToString()), new EqFilterParam("product_id", item.product_id.ToString()));
            return await DataAdapter.UpdateItemAsync(query) > 0 ? true : false;
        }

        public static async Task<PurchaseOrderViewModel> CalcPromo(CustomerViewModel customerModel, string userId, string storeId, string deliveryTypeId)
        {
            try
            {
                var products = await GetAllItemsAsync();
                PurchaseOrderViewModel order = new PurchaseOrderViewModel();
                order.Customer = customerModel;
                order.DeliveryTypeId = Guid.Parse(deliveryTypeId);
                order.PaymentTypeValueId = Guid.Parse("3a27504c-a9ba-46ce-9376-a63403bfe82a");
                order.StoreGuid = Guid.Parse(storeId);
                order.UserId = Guid.Parse(userId);
                foreach (var item in products)
                {
                    PurchaseOrderLineItemViewModel line = new PurchaseOrderLineItemViewModel();
                    line.ProductId = Guid.Parse(item.product_id);
                    line.Qty = item.count;
                    order.LineItems.Add(line);
                }
                var o = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<PurchaseOrderViewModel>(TokenType.AppToken, Configuration.WebService.Purchase.CalcPromo, order);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<PurchaseOrderViewModel> Checkout(CustomerViewModel customerModel, string userId, string storeId, string deliveryTypeId, DeliveryTimeModel time)
        {
            try
            {
                var products = await GetAllItemsAsync();
                PurchaseOrderViewModel order = new PurchaseOrderViewModel();
                order.Customer = customerModel;
                order.DeliveryTypeId = Guid.Parse(deliveryTypeId);
                order.PaymentTypeValueId = Guid.Parse("3a27504c-a9ba-46ce-9376-a63403bfe82a");
                order.StoreGuid = Guid.Parse(storeId);
                order.OrderDate = DateTime.Now;
                order.OrderTime = DateTime.Now.TimeOfDay;
                if (time != null)
                    order.DeliveryFromTime = time.timespan;
                order.PurchaseOrderStatusValueId = Guid.Parse("A591658A-E46B-440D-9ADB-E3E5B01B7489");
                order.UserId = Guid.Parse(userId);
                order.ActionSourceValueId = "65DEC223-059E-48BA-8281-E4FAAFF6E32D";
                foreach (var item in products)
                {
                    PurchaseOrderLineItemViewModel line = new PurchaseOrderLineItemViewModel();
                    line.ProductId = Guid.Parse(item.product_id);
                    line.Qty = item.count;
                    order.LineItems.Add(line);
                }
                var o = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<PurchaseOrderViewModel>(TokenType.AppToken, Configuration.WebService.Purchase.Create, order);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static void OnItemsChanged(Dictionary<ProductModel, int> items)
        {
            if (ItemsChanged != null)
            {
                ItemsChanged.Invoke(items);
            }
        }
        public static event ItemsChangedEventHandler ItemsChanged;
        public delegate void ItemsChangedEventHandler(Dictionary<ProductModel, int> items);
    }
}