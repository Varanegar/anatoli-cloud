﻿using System;

using Foundation;
using UIKit;
using Anatoli.App.Model.Store;

namespace AnatoliIOS.TableViewCells
{
	public partial class StoreSummaryTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("StoreSummaryTableViewCell");
		public static readonly UINib Nib;

		static StoreSummaryTableViewCell ()
		{
			Nib = UINib.FromName ("StoreSummaryTableViewCell", NSBundle.MainBundle);
		}

		public StoreSummaryTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public void UpdateCell(StoreDataModel item){
			if (item != null) {
				storeNameLabel.Text = item.store_name;
			}
		}
	}
}