using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.DAL
{
    public class DataRepository : IDataRepository
    {
        private readonly IMapper _mapper;
        private readonly List<CarModel> _cars;

        public DataRepository(IMapper mapper)
        {
            _cars = new List<CarModel> {
                new CarModel{ Id = 1 },
                new CarModel{ Id = 2 },
                new CarModel{ Id = 5 }
            };

            _mapper = mapper;
        }

        public IEnumerable<CarModel> Get(IEnumerable<int> ids)
        {
            if (ids != null && ids.Any())
                return _cars.Where(x => ids.Contains(x.Id)).ToList();

            return _cars;
        }

        public bool Create(CarModel carDto)
        {
            var model = _mapper.Map<CarModel, CarModel>(carDto);

            model.Id = _cars.Max(x => x.Id) + 1;

            _cars.Add(model);

            return true;
        }

        public bool Update(int id, CarModel model)
        {
            var index = _cars.FindIndex(x => x.Id == id);

            model.Id = id;

            _cars[index] = model;

            return true;
        }

        public bool Delete(int id)
        {
            _cars.RemoveAll(x => x.Id == id);

            return true;
        }
    }
}