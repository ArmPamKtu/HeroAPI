using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.ProductVersions
{
    public class ProductVersionLogic : GenericLogic<IRepository<ProductVersion>, ProductVersionDto, ProductVersion>, IProductVersionLogic
    {
        public ProductVersionLogic(IRepository<ProductVersion> repository) : base(repository)
        {

        }
    }
}
