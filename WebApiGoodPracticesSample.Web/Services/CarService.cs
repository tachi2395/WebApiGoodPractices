using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly IDataRepository<DriverEntity> _driverRepository;

        public CarService(IMapper mapper, IDataRepository<CarEntity> carRepository, IDataRepository<DriverEntity> driverRepository) : base(mapper, carRepository)
        {
            _driverRepository = driverRepository;
        }

        public override IEnumerable<CarEntity> Get(IEnumerable<int> ids)
        {
            var dtos = _dataRepository.Get(ids);

            if (dtos == null || !dtos.Any()) return null;

            foreach (var dto in dtos)
            {
                dto.Drivers = _driverRepository.Get(x => x.CarId == dto.Id);
            }

            return dtos;
        }

        public IEnumerable<DriverEntity> GetDrivers(int id)
        {
            var car = _dataRepository.Get(id);

            if (car == null) return null;

            var drivers = _driverRepository.Get(x => x.CarId == car.Id);

            return drivers;
        }
    }
}
