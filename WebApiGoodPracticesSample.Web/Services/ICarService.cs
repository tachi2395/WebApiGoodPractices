﻿using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Common;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface ICarService : IService<CarEntity>
    {
        public IEnumerable<DriverModel> GetDrivers(int id);
        DriverModel GetDriver(int id, int driverId);
        IEnumerable<CarModel> Get(IEnumerable<int> ids);
        PaginatedModel<CarModel> Get(CarQueryModel query);
    }
}
