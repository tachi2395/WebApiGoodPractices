using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface IDriverService : IService<DriverEntity>
    {
        IEnumerable<DriverModel> Get(List<int> ids);
        IEnumerable<DriverModel> Get(Expression<Func<DriverEntity, bool>> filter);
    }
}
