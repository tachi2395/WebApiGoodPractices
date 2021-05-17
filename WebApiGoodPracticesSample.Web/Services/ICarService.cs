using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Common;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface ICarService : IService<CarEntity>
    {
        public IEnumerable<CarDriverModel> GetDrivers(int driverId);
        CarDriverModel GetDriver(int carId, int driverId);
        IEnumerable<CarModel> Get(IEnumerable<int> ids);
        PaginatedModel<CarModel> Get(CarQueryModel query);
    }
}
