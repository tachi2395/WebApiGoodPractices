using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.Controllers.ActionFilters;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Common;
using WebApiGoodPracticesSample.Web.Services;
using static WebApiGoodPracticesSample.Web.Helpers.UrlBuilderHelper;

namespace WebApiGoodPracticesSample.Web.Controllers
{
    // todo: security

    public class CarsController : ApiBaseController
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        #region GET

        // queryable get
        [HttpGet]
        [Route("")]
        public ActionResult<PaginatedModel<CarModel>> Get([FromQuery] CarQueryModel query)
        {
            var models = _carService.Get(query);

            if (models == null || models.Results == null || !models.Results.Any()) return NotFound();

            return models;
        }

        // get by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult<CarModel> Get([FromRoute] int id)
        {
            var car = _carService.Get(new List<int> { id })?.FirstOrDefault();
            if (car == null) return NotFound();

            return car;
        }

        // get drivers by id
        [HttpGet]
        [Route("{id}/drivers")]
        public ActionResult<IEnumerable<CarDriverModel>> GetDrivers([FromRoute] int id)
        {
            var drivers = _carService.GetDrivers(id);

            if (drivers == null || !drivers.Any()) return NotFound();

            return drivers as List<CarDriverModel>;
        }

        // get driver by car id and driver id
        [HttpGet]
        [Route("{id}/drivers/{driverId}")]
        public ActionResult<CarDriverModel> GetDrivers([FromRoute] int id, [FromRoute] int driverId)
        {
            var driver = _carService.GetDriver(id, driverId);

            if (driver == null || driver == default) return NotFound();

            return driver;
        }
        #endregion

        // create
        [HttpPost]
        [Route("")]
        [ModelValidationFilter]
        public IActionResult Create([FromBody] CreateUpdateCarModel model)
        {
            var carModel = _carService.Create<CreateUpdateCarModel, CarModel>(model);

            if (carModel == null) return UnprocessableEntity();

            return Created(UrlResourceCreated("cars", carModel.Id), carModel);
        }

        // bulk update
        [HttpPut]
        [Route("")]
        [ModelValidationFilter]
        public IActionResult Update([FromBody] IEnumerable<CarModel> models)
        {
            if (_carService.Update(models))
                return NoContent();

            return UnprocessableEntity();
        }

        // update by id
        [HttpPut]
        [Route("{id}")]
        [ModelValidationFilter]
        public IActionResult Update([FromRoute] int id, [FromBody] CreateUpdateCarModel model)
        {
            if (_carService.Update(id, model))
                return NoContent();

            return UnprocessableEntity();
        }

        // delete
        // todo: when delete a cars, drivers are not beign deleted
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_carService.Delete(id))
                return NoContent();

            return UnprocessableEntity();
        }
    }
}
