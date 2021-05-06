using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface ICarService : IService<CarEntity>
    {
        public IEnumerable<DriverEntity> GetDrivers(int id);
    }
}
