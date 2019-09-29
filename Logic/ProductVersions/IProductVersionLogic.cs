using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.ProductVersions
{
    public interface IProductVersionLogic
    {
     

        void Create(FullProductDto combined);
        bool DeleteVersion(Guid id);
        bool UpdateVersion(FullProductDto combined);
        ProductVersionDto GetSpecificProductVersion(Guid id);
        ICollection<FullProductDto> GetAllCombined();
        ProductDto GetProductById(Guid id);
    }
}
