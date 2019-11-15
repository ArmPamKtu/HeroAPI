using Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HeroUnitTests.ProductVersionTests
{
    class ProductVersionTestingData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5 new description", //description changed
                    Name = "Product 5",
                    Cost = 2,
                    ProductId =  new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "",
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 0,
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5 new", //name changed
                    Cost = 2,
                    ProductId =  new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "",
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 0,
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5",
                    Cost = 5,  //cost changed
                    ProductId =  new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "",
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 0,
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5",
                    Cost = 2,
                    ProductId =  new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "new url", //url changed
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 0,
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5",
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "a",
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 8, // quantity changes
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
