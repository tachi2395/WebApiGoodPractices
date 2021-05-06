using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface ICarService : IService<CarModel>
    {
        public IEnumerable<DriverModel> GetDrivers(int id);
    }
}
