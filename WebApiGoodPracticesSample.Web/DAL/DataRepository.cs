using System.Collections.Generic;

namespace WebApiGoodPracticesSample.Web.DAL{
    public class DataRepository : IDataRepository{
        public DataRepository(){}

        public CarModel Get(int id){
            return new CarModel();
        }

        public IEnumerable<CarModel> Get(IEnumerable<int> ids){
            return new List<CarModel>{
                new CarModel()
            };
        }

        public bool Create(CarModel model){
            return true;
        }

        public bool Update(CarModel model){
            return true;
        }

        public bool Delete(int id){
            return true;
        }

        public bool Delete(IEnumerable<int> ids){
            return true;
        }
    }
}