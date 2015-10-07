﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public abstract class BaseDataModel
    {
        public BaseDataModel()
        {

        }
        public string ID { get; set; }
        public Guid UniqueId { get; set; }
        public int SaveReason { get; set; }
        public bool IsAdded { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public bool IsUnchanged { get; set; }
        public bool IsSaveRequired { get; set; }
        public bool ReadOnly { get { return false; } }
        public string DataTable
        {
            get { return GetDataTable(); }
            set { SetDataTable(value); }
        }

        protected void SetDataTable(string value)
        {
            DataTable = value;
        }

        protected abstract string GetDataTable();
    }
}
