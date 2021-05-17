using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Common;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly IDataRepository<DriverEntity> _driverRepository;
        private Dictionary<string, Func<CarEntity, object>> _sortDefinitions = new Dictionary<string, Func<CarEntity, object>>
        {
            { nameof(CarEntity.Id).ToLowerInvariant(), x => x.Id},
            { nameof(CarEntity.Manufacturer).ToLowerInvariant(), x => x.Manufacturer},
            { nameof(CarEntity.Name).ToLowerInvariant(), x => x.Name},
            { nameof(CarEntity.Model).ToLowerInvariant(), x => x.Model},
            { nameof(CarEntity.SerialNumber).ToLowerInvariant(), x => x.SerialNumber},
            { nameof(CarEntity.Color).ToLowerInvariant(), x => x.Color}
        };

        public CarService(IMapper mapper, IDataRepository<CarEntity> carRepository, IDataRepository<DriverEntity> driverRepository) : base(mapper, carRepository)
        {
            _driverRepository = driverRepository;
        }

        public override TModel Get<TModel>(int id)
        {
            return Get<TModel>(new List<int> { id }).FirstOrDefault();
        }

        public IEnumerable<CarModel> Get(IEnumerable<int> ids)
        {
            var carEntities = DataRepository.Get(ids);

            if (carEntities == null || !carEntities.Any()) return null;
            var models = AddDriversAndGetModels(carEntities);

            return models;
        }

        public IEnumerable<DriverModel> GetDrivers(int id)
        {
            var car = DataRepository.Get(id);

            if (car == null) return null;

            var driverEntities = _driverRepository.Get(x => x.CarId == car.Id);

            return Mapper.Map<IEnumerable<DriverEntity>, IEnumerable<DriverModel>>(driverEntities);
        }

        public DriverModel GetDriver(int id, int driverId)
        {
            var car = DataRepository.Get(id);

            if (car == null) return null;

            var drivers = _driverRepository.Get(x => x.CarId == car.Id);

            var driverEntity = drivers?.Where(x => x.Id == driverId)?.FirstOrDefault();

            return Mapper.Map<DriverEntity, DriverModel>(driverEntity);
        }

        public IEnumerable<CarModel> Get(CarQueryModel query)
        {
            var (sort, ascending) = GetSortDefinition(query);
            var filter = BuildFilterExpression(query);

            var entities = DataRepository.Get(filter, x => new CarEntity
            {
                Color = x.Color,
                Drivers = x.Drivers,
                Model = x.Model,
                Id = x.Id,
                Manufacturer = x.Manufacturer,
                Name = x.Name,
                SerialNumber = x.SerialNumber
            }, sort, ascending);

            return AddDriversAndGetModels(entities);
        }

        #region Private methods
        private List<CarModel> AddDriversAndGetModels(IEnumerable<CarEntity> carEntities)
        {
            foreach (var dto in carEntities)
            {
                dto.Drivers = _driverRepository.Get(x => x.CarId == dto.Id);
            }

            var models = Mapper.Map<IEnumerable<CarEntity>, IEnumerable<CarModel>>(carEntities) as List<CarModel>;

            models.ForEach(x => (x.Drivers as List<DriverModel>).ForEach(d =>
            {
                d.Links = new List<LinkObjModel>
                {
                    new LinkObjModel
                    {
                        Rel="self",
                        Href = $"/api/v1/drivers/{d?.Id}"
                    }
                };
            }));
            return models;
        }

        private (Func<CarEntity, object>, bool) GetSortDefinition(CarQueryModel query)
        {
            var sort = _sortDefinitions[nameof(CarEntity.Id).ToLowerInvariant()];
            var ascending = true;
            if (query.Sort.Contains("+") || query.Sort.Contains("-"))
            {
                if (query.Sort.ElementAt(0) == '+')
                    ascending = true;
                else if (query.Sort.ElementAt(0) == '-')
                    ascending = false;
            }

            if (_sortDefinitions.ContainsKey(query.Sort.ToLowerInvariant()))
                sort = _sortDefinitions[query.Sort.ToLowerInvariant()];

            return (sort, ascending);
        }

        private static Expression<Func<CarEntity, bool>> BuildFilterExpression(CarQueryModel query)
        {
            Expression<Func<CarEntity, bool>> filter = x => true;

            // by id
            if (query.Id != null && query.Id.Any())
            {
                var prefix = filter.Compile();
                filter = x => prefix(x) && query.Id.Contains(x.Id);
            }

            // by name
            if (query.Name != null && query.Name.Any())
            {
                var prefix = filter.Compile();
                filter = x => prefix(x) && query.Name.Contains(x.Name);
            }

            // by manufacturer
            if (query.Manufacturer != null && query.Manufacturer.Any())
            {
                var prefix = filter.Compile();
                filter = x => prefix(x) && query.Manufacturer.Contains(x.Manufacturer);
            }

            return filter;
        }
        #endregion
    }
}
