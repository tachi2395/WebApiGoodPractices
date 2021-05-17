using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.DAL
{
    public class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : CommonEntity
    {
        private readonly List<TEntity> _entitites;

        public DataRepository()
        {
            _entitites = new List<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _entitites.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(IEnumerable<int> ids)
        {
            if (ids != null && ids.Any())
                return _entitites.Where(x => ids.Contains((int)x.Id)).ToList();

            return _entitites;
        }

        public (IEnumerable<TEntity> entities, int totalCount) Get(Expression<Func<TEntity, bool>> query, 
            Func<TEntity, TEntity> projectionSelector = null, 
            Func<TEntity, object> sortSelector = null,
            bool orderAscending = true, int page = 0, int pageSize = 0)
        {
            var entities = _entitites
                .Where(query.Compile())
                .ToList();

            if (sortSelector != null)
                entities = orderAscending
                    ? entities.OrderBy(sortSelector).ToList()
                    : entities.OrderByDescending(sortSelector).ToList();

            if (projectionSelector != null)
                entities = entities.Select(projectionSelector).ToList();

            var totalConunt = entities.Count;

            if (pageSize != 0 && page != 0) {
                var skip = (page - 1) * pageSize;
                entities = entities.Skip(skip).Take(pageSize).ToList();
            }

            return (entities, totalConunt);
        }

        public TEntity Create(TEntity model)
        {
            if (_entitites.Any())
                model.Id = _entitites.Max(x => x.Id) + 1;
            else
                model.Id = 1;

            _entitites.Add(model);

            return _entitites.First(x => x.Id == model.Id);
        }

        public bool Update(int id, TEntity model)
        {
            var index = _entitites.FindIndex(x => x.Id == id);

            model.Id = id;

            _entitites[index] = model;

            return true;
        }

        public bool Delete(int id)
        {
            if (_entitites.Exists(x => x.Id == id))
                _entitites.RemoveAll(x => x.Id == id);
            else
                throw new IndexOutOfRangeException();

            return true;
        }
    }
}