using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataRepository _repository;

        public CarController(ILogger<WeatherForecastController> logger, IDataRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<CarDto> Get([FromRoute] int id)
        {
            return Ok(_repository.Get(id));
        }

        [HttpGet]
        public ActionResult<CarDto> Get([FromQuery] IEnumerable<int> ids)
        {
            return Ok(_repository.Get(ids));
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] CarModel model)
        {
            return _repository.Create(model);
        }

        [HttpPut]
        public ActionResult<bool> Update([FromBody] CarModel model){
            return _repository.Update(model);
        }

        [HttpDelete]
        public ActionResult<bool> Delete([FromRoute] int id){
            return _repository.Delete(id);
        }
    }
}
