using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DTO.Cars;
using WebApiGoodPracticesSample.Web.DTO.Drivers;
using WebApiGoodPracticesSample.Web.Model;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly IService<DriverModel> _driverService;

        public CarsController(CarService carService, IService<DriverModel> driverService)
        {
            _carService = carService;
            _driverService = driverService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CarDto> Get([FromRoute] int id)
        {
            return Ok(Get(id));
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<CarDto>> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _carService.Get(ids);
            if (dtos == null || !dtos.Any()) return NotFound();

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}/drivers")]
        public ActionResult<IEnumerable<DriverDto>> GetDrivers([FromRoute] int id)
        {
            var carDto = _carService.Get(id);

            if (carDto == null) return NotFound();

            var drivers = _driverService.Get(x => x.CarId == carDto.Id);

            return Ok(drivers);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<bool> Create([FromBody] CreateUpdateCarDto model)
        {
            return _carService.Create(model);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<bool> Update([FromBody] IEnumerable<CarDto> models)
        {
            return _carService.Update(models);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<bool> Update([FromRoute] int id, [FromBody] CreateUpdateCarDto model)
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
