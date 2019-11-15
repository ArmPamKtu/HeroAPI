using Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroUnitTests
{
    public static class DataForTesting
    {
        public static IQueryable<ProductVersion> GetProductVersions()
        {
            var productVersions = new List<ProductVersion>
            {
                new ProductVersion
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99653"),
                    Created = DateTime.Now,
                    Description = "Product 1",
                    Name = "Product 1",
                    Cost = 5,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = ""
                },
                new ProductVersion
                {

                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99654"),
                    Created = DateTime.Now,
                    Description = "Product 2",
                    Name = "Product 2",
                    Cost = 10,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99660"),
                    SoftDelete = false,
                    UrlImg = ""
                },
                new ProductVersion
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99655"),
                    Created = DateTime.Now,
                    Description = "Product 3",
                    Name = "Product 3 old",
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99661"),
                    SoftDelete = true,
                    UrlImg = ""
                },
                new ProductVersion
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99656"),
                    Created = DateTime.Now,
                    Description = "Product 3",
                    Name = "Product 3 new",
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99662"),
                    SoftDelete = false,
                    UrlImg = ""
                },
                new ProductVersion
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99657"),
                    Created = DateTime.Now,
                    Description = "Product 4",
                    Name = "Product 4",
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99663"),
                    SoftDelete = false,
                    UrlImg = ""
                },
                new ProductVersion
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Created = DateTime.Now,
                    Description = "Product 5",
                    Name = "Product 5",
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99663"),
                    SoftDelete = false,
                    UrlImg = ""
                },
            }.AsQueryable();

            return productVersions;
        }

        public static IQueryable<Product> GetProducts()
        {
            var productVersions = GetProductVersions();
            var products = new List<Product>
            {
                new Product
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    Created = DateTime.Now,
                    IsInStore = true,
                    IsOrderable = true,
                    ProductVersion = productVersions.Where(x=>Guid.Equals( x.ProductId, new Guid("e4e4fccd-04bb-4817-a01a-0f223da99653"))).ToList(),
                    Quantity = 1
                },
                new Product
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99660"),
                    Created = DateTime.Now,
                    IsInStore = true,
                    IsOrderable = false,
                    ProductVersion = productVersions.Where(x=> Guid.Equals( x.ProductId, new Guid("e4e4fccd-04bb-4817-a01a-0f223da99654"))).ToList(),
                    Quantity = 5
                },
                new Product
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99661"),
                    Created = DateTime.Now,
                    IsInStore = false,
                    IsOrderable = true,
                    ProductVersion = productVersions.Where(x=> Guid.Equals( x.ProductId, new Guid("e4e4fccd-04bb-4817-a01a-0f223da99655"))).ToList(),
                    Quantity = 3
                },
                new Product
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99662"),
                    Created = DateTime.Now,
                    IsInStore = true,
                    IsOrderable = false,
                    ProductVersion = productVersions.Where(x=> Guid.Equals( x.ProductId, new Guid("e4e4fccd-04bb-4817-a01a-0f223da99656"))).ToList(),
                    Quantity = 3
                },
                new Product
                {
                    Id = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99663"),
                    Created = DateTime.Now,
                    IsInStore = true,
                    IsOrderable = false,
                    ProductVersion = productVersions.Where(x=> Guid.Equals( x.ProductId, new Guid("e4e4fccd-04bb-4817-a01a-0f223da99657"))).ToList(),
                    Quantity = 0
                }
            }.AsQueryable();

            return products;
        }

    }
}
