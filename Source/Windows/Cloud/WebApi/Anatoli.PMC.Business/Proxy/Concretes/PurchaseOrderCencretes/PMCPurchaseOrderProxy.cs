﻿using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Proxy.Concretes.PurchaseOrder
{
    public class PMCPurchaseOrderProxy : AnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel>, IAnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel>
    {       
        public IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel> PMCSellDetailProxy { get; set; }
        #region Ctors
        public PMCPurchaseOrderProxy() :
            this(AnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>.Create()
            )
        { }

        public PMCPurchaseOrderProxy(IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel> sellDetailProxy
            )
        {
            PMCSellDetailProxy = sellDetailProxy;
        }
        #endregion

        public override PurchaseOrderViewModel Convert(PMCSellViewModel data, PMCStoreConfigEntity storeConfig)
        {
            return new PurchaseOrderViewModel()
            {
                Amount = data.Amount,
                AppOrderNo = data.RequestNo,
                ChargeAmount = data.ChargeAmount,
                Comment = data.Comment,
                DiscountAmount = data.DiscountAmount,
                DiscountAmount2 = data.DiscountAmount2,
                NetAmount = data.NetAmount,
                OrderPDate = data.RequestDateTime,
                PayableAmount = data.PayableAmount,
                TaxAmount = data.TaxAmount,

                LineItems = PMCSellDetailProxy.Convert(data.SellDetail, storeConfig),
            };
        }

        public override PMCSellViewModel ReverseConvert(PurchaseOrderViewModel data, PMCStoreConfigEntity storeConfig)
        {
            int currentCustomerId = CustomerAdapter.Instance.GetCustomerId(data.UserId);
            int fiscalYearId = GeneralCommands.GetFiscalYearId(null);
            return new PMCSellViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                CenterId = storeConfig.CenterId,
                CustomerId = currentCustomerId,
                Amount = data.Amount,
                ChargeAmount = data.ChargeAmount,
                Comment = data.Comment,
                DiscountAmount = data.DiscountAmount,
                DiscountAmount2 = data.DiscountAmount2,
                FiscalYearId = fiscalYearId,
                Freight = data.FreightAmount,
                FreightCustomerAmount = data.FreightAmount,
                InvoiceDate = data.OrderPDate,
                InvoiceDateTime = data.OrderPDate,
                ManualDiscount = 0,
                ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                NetAmount = data.NetAmount,
                PayableAmount = data.PayableAmount,
                RequestNo = data.AppOrderNo,
                RequestDateTime = data.OrderPDate,
                SalesmanId = storeConfig.SalesmanId,
                SellCategoryId = 2, //فاکتور غير حضوري
                SellStatusId = 1, //پيش نويس
                SellTypeId = 1, //1	طبق جدول تخفيفات
                SLId = storeConfig.SLId,
                StockId = storeConfig.StockId,
                TaxAmount = data.TaxAmount,
                IsConfirmed = false,
                PayTypeId = 1, //نقد 


                SellDetail = PMCSellDetailProxy.ReverseConvert(data.LineItems, storeConfig),
            };
        }
    }
}
