﻿using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class ProductPriceModel : BaseViewModel
    {
        public string price { get; set; }
        public string product_id { get; set; }
        public string store_id { get; set; }
    }
}