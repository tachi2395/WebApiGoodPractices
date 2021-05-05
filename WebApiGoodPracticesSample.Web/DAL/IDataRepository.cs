using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.DAL
{
    public interface IDataRepository
    {
        IEnumerable<CarModel> Get(IEnumerable<int> ids);
        
        bool Create(CarModel model);
        
        bool Update(int id, CarModel model);
        
        bool Delete(int id);
    }
}