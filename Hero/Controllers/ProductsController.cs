using Dto;

using Logic.ProductVersions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductVersionLogic _productLogic;

        public ProductsController(IProductVersionLogic logic)
        {
            _productLogic = logic;
        }

        [HttpGet("storeItems")]
        public ActionResult<ICollection<FullProductDto>> GetStoreItems()
        {
            var allProductsCombined = (List<FullProductDto>)_productLogic.GetAllCombined();

            return allProductsCombined;
        }

        [HttpGet("specificProductVersion/{id}")]
        public ActionResult<ProductVersionDto> GetSpecificProductVersion(Guid id)
        {
         
            var chosenProductVersion = _productLogic.GetSpecificProductVersion(id);

            return chosenProductVersion;
        }

        [HttpPost]
        async public Task<ActionResult> Post([FromBody] FullProductDto fullproductDto)
        {

            //split full into product and version, and add them


            _productLogic.Create(fullproductDto);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody]  FullProductDto fullproductDto)
        {

            _productLogic.UpdateVersion(fullproductDto);
            return Ok();
         
        }

        [HttpDelete("deleteProductVersion/{id}")]
        public ActionResult DeleteProductVersion(Guid id)
        {

            _productLogic.DeleteVersion(id);

            return Ok();
        }

    }
}
