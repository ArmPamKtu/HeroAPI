using Dto;
using Logic.Feats;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatsController : ControllerBase
    {
        private readonly IFeatLogic _featLogic;

        public FeatsController(IFeatLogic logic)
        {
            _featLogic = logic;
        }

        [HttpGet]
        public ActionResult<ICollection<FeatDto>> Get()
        {
            var feats = _featLogic.GetAll().ToList();
            return feats;
        }

        [HttpGet("{id}")]
        public ActionResult<FeatDto> GetById(Guid id)
        {
            var feat = _featLogic.GetById(id);
            return feat;
        }

        [HttpPost]
        async public Task<ActionResult> Post([FromBody] FeatDto featDto)
        {
            
            _featLogic.Create(featDto);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] FeatDto featDto)
        {
            
            bool update = _featLogic.Update(featDto.Id, featDto);
            if (update == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {

            bool delete = _featLogic.Delete(id);
            if (delete == true)
                return Ok();
            else
                return BadRequest();
        }

    }
}
