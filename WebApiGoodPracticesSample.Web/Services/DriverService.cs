using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Helpers;
using WebApiGoodPracticesSample.Web.Model.Common;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class DriverService : Service<DriverEntity>, IDriverService
    {
        private readonly IDataRepository<CarEntity> _carRepo;

        public DriverService(IMapper mapper, IDataRepository<DriverEntity> driverRepository, IDataRepository<CarEntity> carRepo) : base(mapper, driverRepository)
        {
            _carRepo = carRepo;
        }

        public IEnumerable<DriverModel> Get(List<int> ids)
        {
            IEnumerable<DriverEntity> entities = null;
            if (ids != null && ids.Any())
                (entities, _) = DataRepository.Get(x => ids.Contains((int)x.Id));
            else
                (entities, _) = DataRepository.Get(x => true);


            var models = Mapper.Map<List<DriverEntity>, List<DriverModel>>(entities as List<DriverEntity>);

            models.ForEach(x =>
            {
                var carEntity = _carRepo.Get(car => car.Id == x.CarId).entities?.FirstOrDefault();
                x.Car = Mapper.Map<CarEntity, DriverCarModel>(carEntity);

                x.Car.Links = new List<LinkObjModel>
                {
                    new LinkObjModel
                    {
                        Rel = "self",
                        Href = UrlBuilderHelper.UrlResourceCreated("cars", x.CarId)
                    }
                };
            });

            return models;
        }
    }
}
