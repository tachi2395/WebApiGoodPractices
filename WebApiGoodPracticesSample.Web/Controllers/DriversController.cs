using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Drivers;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    public class DriversController : ApiBaseController
    {
        private readonly IService<DriverEntity> _driverService;

        public DriversController(IService<DriverEntity> driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DriverModel> Get([FromRoute] int id)
        {
            var dto = _driverService.Get<DriverModel>(id);

            if (dto == null) return NotFound();

            return dto;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<DriverModel>> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _driverService.Get<DriverModel>(ids);

            if (dtos == null || !dtos.Any()) return NotFound();

            return dtos as List<DriverModel>;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<bool> Create([FromBody] CreateUpdateDriverModel model)
        {
            return _driverService.Create(model);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<bool> Update([FromBody] IEnumerable<DriverModel> models)
        {
            return _driverService.Update(models);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<bool> Update([FromRoute] int id, [FromBody] CreateUpdateDriverModel model)
        {
            return _driverService.Update(id, model);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<bool> Delete([FromRoute] int id)
        {
            return _driverService.Delete(id);
        }
    }
}
