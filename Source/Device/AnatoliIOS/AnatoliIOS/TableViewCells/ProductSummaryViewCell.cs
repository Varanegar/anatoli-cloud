﻿using System;

using Foundation;
using UIKit;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Manager;
using System.Drawing;

namespace AnatoliIOS.TableViewCells
{
	public partial class ProductSummaryViewCell : BaseTableViewCell
	{
		public static readonly NSString Key = new NSString ("ProductSummaryViewCell");
		public static readonly UINib Nib;

		static ProductSummaryViewCell ()
		{
			Nib = UINib.FromName ("ProductSummaryViewCell", NSBundle.MainBundle);
		}
		public ProductSummaryViewCell (IntPtr handle) : base (handle)
		{

		}


		public void BindCell(ProductModel item){
			addProductButton.TouchUpInside += async (object sender, EventArgs e) => {
				addProductButton.Enabled = false;
				if (item.count +1 > item.qty) {
					var alert = UIAlertController.Create("خطا","موجودی کافی نیست",UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("باشه",UIAlertActionStyle.Default,null));
					AnatoliApp.GetInstance().PresentViewController(alert);
					addProductButton.Enabled = true;
					return;
				}
				var result = await ShoppingCardManager.AddProductAsync(item);
				addProductButton.Enabled = true;
				if (result) {
					toolsView.Hidden = false;
					countLabel.Text = item.count.ToString() + " عدد";
					Console.WriteLine(ShoppingCardManager.GetTotalPriceAsync());
				}
			};
			removeProductButton.TouchUpInside += async (object sender, EventArgs e) => {
				if (item.count > 0) {
					var result = await ShoppingCardManager.RemoveProductAsync(item);
					if (result) {
						countLabel.Text = item.count.ToString() + " عدد";
						if (item.count == 0) {
							toolsView.Hidden = true;
							OnItemRemoved();
						}
					}
				}
			};
		}
		public void UpdateCell(ProductModel item){
			productLabel.Text = item.product_name;

			if (item.count > 0) {
				toolsView.Hidden = false;
				countLabel.Text = item.count.ToString() + " عدد";
			} else {
				toolsView.Hidden = true;
			}
			var imgUri = ProductManager.GetImageAddress (item.product_id, item.image);
			if (imgUri != null) {
				try {
					//productImageView.SetImage(url : new NSUrl(imgUri),placeholder: UIImage.FromBundle ("igicon"));
				} catch (Exception) {
					
				}
			}

			if (!item.IsAvailable) {
				addProductButton.Enabled = false;
				productLabel.TextColor = UIColor.Gray;
				priceLabel.TextColor = UIColor.Gray;
				priceLabel.Text = "موجود نیست";
			} else {
				addProductButton.Enabled = true;
				productLabel.TextColor = UIColor.Black;
				priceLabel.TextColor = UIColor.Black;
				priceLabel.Text = item.price.ToCurrency () + " تومان";
			}


		}
	}
}
