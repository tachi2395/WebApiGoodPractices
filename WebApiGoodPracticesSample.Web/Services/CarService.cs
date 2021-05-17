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

            var getResponse = _driverRepository.Get(x => x.CarId == car.Id);

            return Mapper.Map<IEnumerable<DriverEntity>, IEnumerable<DriverModel>>(getResponse.entities);
        }

        public DriverModel GetDriver(int id, int driverId)
        {
            var car = DataRepository.Get(id);

            if (car == null) return null;

            var getResponse = _driverRepository.Get(x => x.CarId == car.Id);
            var drivers = getResponse.entities;

            var driverEntity = drivers?.Where(x => x.Id == driverId)?.FirstOrDefault();

            return Mapper.Map<DriverEntity, DriverModel>(driverEntity);
        }

        public PaginatedModel<CarModel> Get(CarQueryModel query)
        {
            var (sort, ascending) = GetSortDefinition(query);
            var filter = BuildFilterExpression(query);
            var projection = GetProjection(query);

            var getResponse = DataRepository.Get(filter, projection, sort, ascending, query.Page, query.PageSize);

            return new PaginatedModel<CarModel>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = getResponse.totalCount,
                Results = AddDriversAndGetModels(getResponse.entities)
            };
        }

        #region Private methods
        private static Func<CarEntity, CarEntity> GetProjection(CarQueryModel query)
        {
            Func<CarEntity, CarEntity> projection = null;

            if (query.Field != null && query.Field.Any())
                projection = x => new CarEntity
                {
                    Color = query.Field.Contains(nameof(x.Color).ToLowerInvariant()) ? x.Color : null,
                    Drivers = query.Field.Contains(nameof(x.Drivers).ToLowerInvariant()) ? x.Drivers : null,
                    Model = query.Field.Contains(nameof(x.Model).ToLowerInvariant()) ? x.Model : null,
                    Id = query.Field.Contains(nameof(x.Id).ToLowerInvariant()) ? x.Id : null,
                    Manufacturer = query.Field.Contains(nameof(x.Manufacturer).ToLowerInvariant()) ? x.Manufacturer : null,
                    Name = query.Field.Contains(nameof(x.Name).ToLowerInvariant()) ? x.Name : null,
                    SerialNumber = query.Field.Contains(nameof(x.SerialNumber).ToLowerInvariant()) ? x.SerialNumber : null
                };

            return projection;
        }

        private List<CarModel> AddDriversAndGetModels(IEnumerable<CarEntity> carEntities)
        {
            foreach (var dto in carEntities)
            {
                var getResponse = _driverRepository.Get(x => x.CarId == dto.Id);
                dto.Drivers = getResponse.entities;
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

            models.ForEach(x => x.Drivers = x.Drivers != null && x.Drivers.Any() ? x.Drivers : null);

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
                filter = x => prefix(x) && query.Id.Contains((int)x.Id);
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
