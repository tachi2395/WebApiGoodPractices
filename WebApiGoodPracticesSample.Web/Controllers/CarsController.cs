using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DTO;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICarService _carService;

        public CarsController(ILogger<WeatherForecastController> logger, ICarService repository)
        {
            _logger = logger;
            _carService = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CarDto> Get([FromRoute] int id)
        {
            var dto = _carService.Get(id);

            if (dto == null) return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        [Route("")]
        public ActionResult<CarDto> Get([FromQuery(Name = "id")] IEnumerable<int> ids)
        {
            var dtos = _carService.Get(ids);

            if (dtos == null || !dtos.Any()) return NotFound();

            return Ok(dtos);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<bool> Create([FromBody] CreateCarDto model)
        {
            return _carService.Create(model);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<bool> Update([FromBody] IEnumerable<BulkUpdateCarDto> models)
        {
            return _carService.Update(models);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<bool> Update([FromRoute] int id, [FromBody] SingleUpdateCarDto model)
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
