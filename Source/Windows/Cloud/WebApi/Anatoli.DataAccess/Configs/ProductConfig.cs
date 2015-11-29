using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            this.HasRequired<ProductBase>(pb => pb.ProductBase)
                .WithMany(p => p.Products);

            this.HasOptional<ProductGroup>(pg => pg.ProductGroup)
                .WithMany(m => m.Products);

            this.HasMany<ProductPicture>(pp => pp.ProductPictures)
                .WithRequired(p => p.Product);

            this.HasMany<ProductRate>(pr => pr.ProductRates)
                .WithRequired(p => p.Product);

            this.HasMany<ProductComment>(pc => pc.ProductComments)
                .WithRequired(p => p.Product);


            this.HasMany<CharValue>(s => s.CharValues)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductId");
                    cs.MapRightKey("CharValueID");
                    cs.ToTable("ProductBase_ProductMap");
                });

            this.HasMany<StoreActivePriceList>(sap => sap.StoreActivePriceLists)
                .WithRequired(p => p.Product);

        }
    }
}