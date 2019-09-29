using Db.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.ProductVersions
{
    public static class ProductCombinedExtension
    {
        public static FullProductDto ToCombinedProduct(this ProductVersionDto version, ProductDto product)
        {
            FullProductDto newProduct = new FullProductDto();

            newProduct.ProductVersionId = version.Id;
            newProduct.Cost = version.Cost;
            newProduct.ProductVersionCreated = version.Created;
            newProduct.Description = version.Description;
            newProduct.IsInStore = product.IsInStore;
            newProduct.ProductCreated = product.Created;
            newProduct.Name = version.Name;
            newProduct.UrlImg = version.UrlImg;
            newProduct.IsOrderable = product.IsOrderable;
            newProduct.Quantity = product.Quantity;
            newProduct.SoftDelete = version.SoftDelete;
            newProduct.ProductId = version.ProductId;
            return newProduct;
        }
        public static void SplitCombinedProduct(this FullProductDto combined, out ProductVersion version, out Product product)
        {
            version = new ProductVersion();
            product = new Product();

            version.Cost = combined.Cost;
            version.Created = DateTime.Now;
            version.Description = combined.Description;
            version.Name = combined.Name;
            version.Product = product;
            version.SoftDelete = combined.SoftDelete;
            version.UrlImg = combined.UrlImg;
            version.Product = product;

            product.IsOrderable = combined.IsOrderable;
            product.Created = DateTime.Now;
            product.IsInStore = combined.IsInStore;
            product.Quantity = combined.Quantity;
        }
    }
}
