using Dto;
using Logic.Exceptions;
using Logic.ProductVersions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        public ActionResult<ICollection<FullProductDto>> GetStoreItems()
        {
            var allProductsCombined = (List<FullProductDto>)_productLogic.GetAllCombined();

            return allProductsCombined;
        }

        [HttpGet("specificProductVersion/{id}")]
        [Authorize(Roles = "Manager")]
        public ActionResult<ProductVersionDto> GetSpecificProductVersion(Guid id)
        {
         
            var chosenProductVersion = _productLogic.GetSpecificProductVersion(id);

            return chosenProductVersion;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        async public Task<ActionResult> Post([FromBody] FullProductDto fullproductDto)
        {
            fullproductDto.ProductCreated = DateTime.Now;
            fullproductDto.ProductVersionCreated = DateTime.Now;
            _productLogic.Create(fullproductDto);
            return Ok();
        }


        [HttpPut]
        [Authorize(Roles = "Manager")]
        public ActionResult Update([FromBody]  FullProductDto fullproductDto)
        {

            _productLogic.UpdateVersion(fullproductDto);
            return Ok();
         
        }

        [HttpDelete("deleteProductVersion/{id}")]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteProductVersion(Guid id)
        {

            _productLogic.DeleteVersion(id);

            return Ok();
        }

    }
}
