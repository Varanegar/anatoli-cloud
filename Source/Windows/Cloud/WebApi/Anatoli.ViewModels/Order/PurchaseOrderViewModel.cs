﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.Order
{
    public class PurchaseOrderViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }
        public Guid StoreGuid { get; set; }
        public Guid PurchaseOrderStatusValueId { get; set; }
        public Guid PaymentTypeValueId { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public string OrderPDate { get; set; }
        public string DeviceIMEI { get; set; }
        public Guid ActionSourceValueId { get; set; }
        public String Comment { get; set; }
        public string AppOrderNo { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountAmount2 { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal FinalDiscountAmount { get; set; }
        public decimal FinalTaxAmount { get; set; }
        public decimal FinalChargeAmount { get; set; }
        public decimal FinalDiscountAmount2 { get; set; }
        public decimal FinalPayableAmount { get; set; }
        public decimal FinalFreightAmount { get; set; }
        public Guid DeliveryTypeId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryPDate { get; set; }
        public TimeSpan? DeliveryFromTime { get; set; }
        public TimeSpan? DeliveryToTime { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelReason { get; set; }

        List<PurchaseOrderLineItem> LineItem = new List<PurchaseOrderLineItem>();

    }
}