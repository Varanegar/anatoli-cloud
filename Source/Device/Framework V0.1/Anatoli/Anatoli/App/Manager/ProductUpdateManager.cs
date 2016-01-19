﻿using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class ProductUpdateManager : BaseManager<BaseDataAdapter<ProductUpdateModel>, ProductUpdateModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            //return await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<List<ProductModel>>(TokenType.AppToken, Configuration.WebService.Products.ProductsView);
            try
            {
                var lastUpdateTiem = await SyncManager.GetLastUpdateDateAsync("products");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Products.ProductsView);
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                int c = await LocalUpdateAsync(new DeleteCommand("products"));
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        InsertCommand command = new InsertCommand("products", new BasicParam("product_id", item.UniqueId.ToUpper()),
                            new BasicParam("product_name", item.ProductName),
                            new BasicParam("cat_id", (item.ProductGroupIdString != null) ? item.ProductGroupIdString.ToUpper() : item.ProductGroupIdString));
                        var query = connection.CreateCommand(command.GetCommand());
                        int t = query.ExecuteNonQuery();
                    }
                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync("products");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
