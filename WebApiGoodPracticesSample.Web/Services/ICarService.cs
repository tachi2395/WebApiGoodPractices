using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DTO;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface ICarService
    {
        // get
        CarModel Get(int id);
        IEnumerable<CarModel> Get(IEnumerable<int> ids);

        // create
        bool Create(CreateCarDto dto);

        // update
        bool Update(IEnumerable<BulkUpdateCarDto> cars);
        bool Update(int id, SingleUpdateCarDto model);

        // delete
        bool Delete(int id);
    }
}
