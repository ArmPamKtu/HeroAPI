using System;
using System.Collections.Generic;
using System.Text;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;

namespace Logic.Products
{
    public class ProductLogic : GenericLogic<IRepository<Product>, ProductDto, Product>, IProductLogic
    {
        public ProductLogic(IRepository<Product> repository) : base(repository)
        {

        }
    }
}
