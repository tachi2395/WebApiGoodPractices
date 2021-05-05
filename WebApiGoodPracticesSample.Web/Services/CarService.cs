using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DTO;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _dataRepository;

        public CarService(IMapper mapper, IDataRepository dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        public bool Create(CreateCarDto dto)
        {
            try
            {
                var model = _mapper.Map<CreateCarDto, CarModel>(dto);

                return _dataRepository.Create(model);
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return _dataRepository.Delete(id);
            }
            catch
            {
                return false;
            }
        }

        public CarModel Get(int id)
        {
            try
            {
                return _dataRepository.Get(new List<int> { id }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<CarModel> Get(IEnumerable<int> ids)
        {
            try
            {
                return _dataRepository.Get(ids);
            }
            catch
            {
                return null;
            }
        }

        public bool Update(IEnumerable<BulkUpdateCarDto> cars)
        {
            try
            {
                foreach (var car in cars)
                {
                    var model = _mapper.Map<BulkUpdateCarDto, CarModel>(car);
                    var updateResult = _dataRepository.Update(model.Id, model);

                    if (!updateResult) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, SingleUpdateCarDto dto)
        {
            try
            {
                var model = _mapper.Map<SingleUpdateCarDto, CarModel>(dto);
                return _dataRepository.Update(id, model);
            }
            catch
            {
                return false;
            }
        }
    }
}
