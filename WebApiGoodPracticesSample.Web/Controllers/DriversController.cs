using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Drivers;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IService<DriverEntity> _carService;

        public DriversController(IService<DriverEntity> repository)
        {
            _carService = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DriverModel> Get([FromRoute] int id)
        {
            var dto = _carService.Get(id);

            if (dto == null) return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        [Route("")]
        public ActionResult<DriverModel> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _carService.Get(ids);

            if (dtos == null || !dtos.Any()) return NotFound();

            return Ok(dtos);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<bool> Create([FromBody] CreateUpdateDriverModel model)
        {
            return _carService.Create(model);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<bool> Update([FromBody] IEnumerable<DriverModel> models)
        {
            return _carService.Update(models);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<bool> Update([FromRoute] int id, [FromBody] CreateUpdateDriverModel model)
        {
            return _carService.Update(id, model);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<bool> Delete([FromRoute] int id)
        {
            return _carService.Delete(id);
        }
    }
}
