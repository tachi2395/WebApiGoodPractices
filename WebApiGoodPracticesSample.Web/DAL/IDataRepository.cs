using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.DAL
{
    public interface IDataRepository<TEntity> where TEntity : CommonEntity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> Get(IEnumerable<int> ids);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> query, Func<TEntity, TEntity> selector = null, Func<TEntity, object> keySelector = null, bool ascending = true);

        TEntity Create(TEntity entity);

        bool Update(int id, TEntity entity);

        bool Delete(int id);
    }
}