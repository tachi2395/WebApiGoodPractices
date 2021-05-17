using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Common;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly IDriverService _driverService;

        private Dictionary<string, Func<CarEntity, object>> _sortDefinitions = new Dictionary<string, Func<CarEntity, object>>
        {
            { nameof(CarEntity.Id).ToLowerInvariant(), x => x.Id},
            { nameof(CarEntity.Manufacturer).ToLowerInvariant(), x => x.Manufacturer},
            { nameof(CarEntity.Name).ToLowerInvariant(), x => x.Name},
            { nameof(CarEntity.Model).ToLowerInvariant(), x => x.Model},
            { nameof(CarEntity.SerialNumber).ToLowerInvariant(), x => x.SerialNumber},
            { nameof(CarEntity.Color).ToLowerInvariant(), x => x.Color}
        };

        public CarService(IMapper mapper, IDataRepository<CarEntity> carRepository, IDriverService driverService) : base(mapper, carRepository)
        {
            _driverService = driverService;
        }

        public IEnumerable<CarModel> Get(IEnumerable<int> ids)
        {
            var carEntities = DataRepository.Get(ids);

            if (carEntities == null || !carEntities.Any()) return null;
            var models = AddDriversAndGetModels(carEntities, true);

            return models;
        }

        public IEnumerable<CarDriverModel> GetDrivers(int carId)
        {
            var driverModels = _driverService.Get(x => x.CarId == carId);

            return Mapper.Map<IEnumerable<Model.Drivers.DriverModel>, IEnumerable<CarDriverModel>>(driverModels);
        }

        public CarDriverModel GetDriver(int carId, int driverId)
        {
            var driverModels = _driverService.Get(x => x.CarId == carId && x.Id == driverId);

            return Mapper.Map<Model.Drivers.DriverModel, CarDriverModel>(driverModels?.FirstOrDefault());
        }

        public PaginatedModel<CarModel> Get(CarQueryModel query)
        {
            var (sort, ascending) = GetSortDefinition(query);
            var filter = BuildFilterExpression(query);
            var (sortDefinition, includeDrivers) = GetProjection(query);

            var getResponse = DataRepository.Get(filter, sortDefinition, sort, ascending, query.Page, query.PageSize);

            return new PaginatedModel<CarModel>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = getResponse.totalCount,
                Results = AddDriversAndGetModels(getResponse.entities, includeDrivers)
            };
        }

        #region Private methods
        private static (Func<CarEntity, CarEntity> sortDefinition, bool includeDrivers) GetProjection(CarQueryModel query)
        {
            Func<CarEntity, CarEntity> projection = null;
            var includeDrivers = true;

            if (query.Field != null && query.Field.Any())
            {
                projection = x => new CarEntity
                {
                    Id = x.Id,
                    Color = query.Field.Contains(nameof(x.Color).ToLowerInvariant()) ? x.Color : null,
                    Model = query.Field.Contains(nameof(x.Model).ToLowerInvariant()) ? x.Model : null,
                    Manufacturer = query.Field.Contains(nameof(x.Manufacturer).ToLowerInvariant()) ? x.Manufacturer : null,
                    Name = query.Field.Contains(nameof(x.Name).ToLowerInvariant()) ? x.Name : null,
                    SerialNumber = query.Field.Contains(nameof(x.SerialNumber).ToLowerInvariant()) ? x.SerialNumber : null
                };

                includeDrivers = query.Field.Contains(nameof(CarModel.Drivers).ToLowerInvariant());
            }

            return (projection, includeDrivers);
        }

        private List<CarModel> AddDriversAndGetModels(IEnumerable<CarEntity> carEntities, bool includeDrivers)
        {
            // mapping to model
            var models = Mapper.Map<IEnumerable<CarEntity>, IEnumerable<CarModel>>(carEntities) as List<CarModel>;

            if (!includeDrivers) return models;

            // getting drivers for each car
            models.Clear();
            foreach (var carEntitie in carEntities)
            {
                var driverModels = _driverService.Get(x => x.CarId == carEntitie.Id);

                var carModel = Mapper.Map<CarEntity, CarModel>(carEntitie);
                carModel.Drivers = Mapper.Map<IEnumerable<Model.Drivers.DriverModel>, IEnumerable<CarDriverModel>>(driverModels);

                models.Add(carModel);
            }

            // setting hateoas for each d
            models.ForEach(x => (x.Drivers as List<CarDriverModel>).ForEach(d =>
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

            // removing empty drivers
            models.ForEach(x => x.Drivers = x.Drivers != null && x.Drivers.Any() ? x.Drivers : null);

            return models;
        }

        private (Func<CarEntity, object>, bool) GetSortDefinition(CarQueryModel query)
        {
            var sort = _sortDefinitions[nameof(CarEntity.Id).ToLowerInvariant()];

            var sortOperator = query.Sort.Substring(0, 1);
            var sortField = query.Sort.Substring(1).ToLowerInvariant();

            var ascending = sortOperator == "+"
                ? true
                : sortOperator != "-";

            if (_sortDefinitions.ContainsKey(sortField))
                sort = _sortDefinitions[sortField];

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
