using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : Service<CarModel>
    {
        private readonly IDataRepository<DriverModel> _driverRepository;

        public CarService(IMapper mapper, IDataRepository<CarModel> carRepository, IDataRepository<DriverModel> driverRepository) : base(mapper, carRepository)
        {
            _driverRepository = driverRepository;
        }

        public override IEnumerable<CarModel> Get(IEnumerable<int> ids)
        {
            var dtos = _dataRepository.Get(ids);

            if (dtos == null || !dtos.Any()) return null;

            foreach (var dto in dtos)
            {
                dto.Drivers = _driverRepository.Get(x => x.CarId == dto.Id);
            }

            return dtos;
        }
    }
}
