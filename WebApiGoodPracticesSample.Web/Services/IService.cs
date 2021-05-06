using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface IService<TModel> where TModel : CommonEntity
    {
        // get
        TModel Get(int id);
        IEnumerable<TModel> Get(IEnumerable<int> ids);
        IEnumerable<TModel> Get(Expression<Func<TModel, bool>> query);

        // create
        bool Create<TDto>(TDto dto);

        // update
        bool Update<TDto>(IEnumerable<TDto> cars);
        bool Update<TDto>(int id, TDto model);

        // delete
        bool Delete(int id);
    }
}
