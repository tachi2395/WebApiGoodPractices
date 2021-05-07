using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public interface IService<TEntity> where TEntity : CommonEntity
    {
        // get
        TModel Get<TModel>(int id);
        IEnumerable<TModel> Get<TModel>(IEnumerable<int> ids);
        IEnumerable<TModel> Get<TModel>(Expression<Func<TEntity, bool>> query);

        // create
        TModelOut Create<TModelIn, TModelOut>(TModelIn model);

        // update
        bool Update<TModel>(IEnumerable<TModel> cars);
        bool Update<TModel>(int id, TModel model);

        // delete
        bool Delete(int id);
    }
}
