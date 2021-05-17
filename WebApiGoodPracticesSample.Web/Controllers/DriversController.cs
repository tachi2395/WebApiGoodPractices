using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.Controllers.ActionFilters;
using WebApiGoodPracticesSample.Web.Model.Drivers;
using WebApiGoodPracticesSample.Web.Services;
using static WebApiGoodPracticesSample.Web.Helpers.UrlBuilderHelper;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    public class DriversController : ApiBaseController
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DriverModel> Get([FromRoute] int id)
        {
            var dto = _driverService.Get(new List<int> { id })?.FirstOrDefault();

            if (dto == null) return NotFound();

            return dto;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<DriverModel>> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _driverService.Get(ids as List<int>);

            if (dtos == null || !dtos.Any()) return NotFound();

            return dtos as List<DriverModel>;
        }

        [HttpPost]
        [Route("")]
        [ModelValidationFilter]
        public IActionResult Create([FromBody] CreateUpdateDriverModel model)
        {
            var driverModel = _driverService.Create<CreateUpdateDriverModel, DriverModel>(model);

            if (driverModel == null) return UnprocessableEntity();

            return Created(UrlResourceCreated("drivers", driverModel.Id), driverModel);
        }

        [HttpPut]
        [Route("")]
        [ModelValidationFilter]
        public IActionResult Update([FromBody] IEnumerable<DriverModel> models)
        {
            if (_driverService.Update(models))
                return NoContent();

            return UnprocessableEntity();
        }

        [HttpPut]
        [Route("{id}")]
        [ModelValidationFilter]
        public IActionResult Update([FromRoute] int id, [FromBody] CreateUpdateDriverModel model)
        {
            if (_driverService.Update(id, model))
                return NoContent();

            return UnprocessableEntity();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_driverService.Delete(id))
                return NoContent();

            return UnprocessableEntity();
        }
    }
}
