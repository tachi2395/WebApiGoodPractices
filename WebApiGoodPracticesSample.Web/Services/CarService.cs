using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly IDataRepository<DriverEntity> _driverRepository;

        public CarService(IMapper mapper, IDataRepository<CarEntity> carRepository, IDataRepository<DriverEntity> driverRepository) : base(mapper, carRepository)
        {
            _driverRepository = driverRepository;
        }

        public override TModel Get<TModel>(int id)
        {
            return Get<TModel>(new List<int> { id }).FirstOrDefault();
        }

        public override IEnumerable<TModel> Get<TModel>(IEnumerable<int> ids)
        {
            var carEntities = DataRepository.Get(ids);

            if (carEntities == null || !carEntities.Any()) return null;

            foreach (var dto in carEntities)
            {
                dto.Drivers = _driverRepository.Get(x => x.CarId == dto.Id);
            }

            return Mapper.Map<IEnumerable<CarEntity>, IEnumerable<TModel>>(carEntities);
        }

        public IEnumerable<DriverModel> GetDrivers(int id)
        {
            var car = DataRepository.Get(id);

            if (car == null) return null;

            var driverEntities = _driverRepository.Get(x => x.CarId == car.Id);

            return Mapper.Map<IEnumerable<DriverEntity>, IEnumerable<DriverModel>>(driverEntities);
        }

        public DriverModel GetDriver(int id, int driverId)
        {
            var car = DataRepository.Get(id);

            if (car == null) return null;

            var drivers = _driverRepository.Get(x => x.CarId == car.Id);

            var driverEntity = drivers?.Where(x => x.Id == driverId)?.FirstOrDefault();

            return Mapper.Map<DriverEntity, DriverModel>(driverEntity);
        }
    }
}
