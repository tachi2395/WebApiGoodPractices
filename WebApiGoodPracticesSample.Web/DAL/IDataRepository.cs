using System.Collections.Generic;

namespace WebApiGoodPracticesSample.Web.DAL{
    public interface IDataRepository{
        CarModel Get(int id);
        IEnumerable<CarModel> Get(IEnumerable<int> ids);
        bool Create(CarModel model);
        bool Update(CarModel model);
        bool Delete(int id);
        bool Delete(IEnumerable<int> ids);
    }
}