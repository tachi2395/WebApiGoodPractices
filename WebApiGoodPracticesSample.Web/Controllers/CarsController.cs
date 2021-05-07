using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Drivers;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    public class CarsController : ApiBaseController
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<CarModel>> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _carService.Get<CarModel>(ids);
            if (dtos == null || !dtos.Any()) return NotFound();

            return dtos as List<CarModel>;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CarModel> Get([FromRoute] int id)
        {
            var car = _carService.Get<CarModel>(id);
            if (car == null) return NotFound();

            return car;
        }

        [HttpGet]
        [Route("{id}/drivers")]
        public ActionResult<IEnumerable<DriverModel>> GetDrivers([FromRoute] int id)
        {
            var drivers = _carService.GetDrivers(id);

            if (drivers == null || !drivers.Any()) return NotFound();

            return drivers as List<DriverModel>;
        }

        [HttpGet]
        [Route("{id}/drivers/{driverId}")]
        public ActionResult<DriverModel> GetDrivers([FromRoute] int id, [FromRoute] int driverId)
        {
            var driver = _carService.GetDriver(id, driverId);

            if (driver == null || driver == default) return NotFound();

            return driver;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<bool> Create([FromBody] CreateUpdateCarModel model)
        {
            return _carService.Create(model);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<bool> Update([FromBody] IEnumerable<CarModel> models)
        {
            return _carService.Update(models);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<bool> Update([FromRoute] int id, [FromBody] CreateUpdateCarModel model)
        {
            return _carService.Update(id, model);
        }

        // todo: when delete a cars, drivers are not beign deleted
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<bool> Delete([FromRoute] int id)
        {
            return _carService.Delete(id);
        }
    }
}
