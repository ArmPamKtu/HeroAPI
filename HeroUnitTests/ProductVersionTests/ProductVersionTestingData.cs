using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dto;

namespace HeroUnitTests.ProductVersionTests
{
    public class ProductVersionTestingData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
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
                    UrlImg = "",
                    IsInStore = true,
                    IsOrderable = false,
                    Quantity = 5, // quantity changed
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
            yield return new object[]
            {
                new FullProductDto
                {
                    ProductVersionId =   new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5", 
                    Cost = 2,
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "",
                    IsInStore = false, // changes status
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
                    ProductVersionId =  new Guid("e4e4fccd-04bb-4817-a01a-0f223da99658"),
                    Description = "Product 5",
                    Name = "Product 5",
                    Cost = 2,  
                    ProductId = new Guid("e4e4fccd-04bb-4817-a01a-0f223da99659"),
                    SoftDelete = false,
                    UrlImg = "",
                    IsInStore = true,
                    IsOrderable = true, // changed order status
                    Quantity = 0,
                    ProductCreated = DateTime.Now,
                    ProductVersionCreated = DateTime.Now,
                }
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

