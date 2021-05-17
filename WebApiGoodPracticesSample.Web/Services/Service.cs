using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : CommonEntity
    {
        protected readonly IMapper Mapper;
        protected readonly IDataRepository<TEntity> DataRepository;

        public Service(IMapper mapper, IDataRepository<TEntity> dataRepository)
        {
            Mapper = mapper;
            DataRepository = dataRepository;
        }

        public TModelOut Create<TModelIn, TModelOut>(TModelIn model)
        {
            try
            {
                var entityModel = Mapper.Map<TModelIn, TEntity>(model);

                var entity = DataRepository.Create(entityModel);

                return Mapper.Map<TEntity, TModelOut>(entity);
            }
            catch
            {
                return default;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return DataRepository.Delete(id);
            }
            catch
            {
                return false;
            }
        }

        #region GET
        public virtual TModel Get<TModel>(int id)
        {
            try
            {
                var entity = DataRepository.Get(id);

                return Mapper.Map<TEntity, TModel>(entity);
            }
            catch
            {
                return default;
            }
        }

        public virtual IEnumerable<TModel> Get<TModel>(IEnumerable<int> ids)
        {
            try
            {
                var entities = DataRepository.Get(ids);

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<TModel> Get<TModel>(Expression<Func<TEntity, bool>> query)
        {
            try
            {
                var entities = DataRepository.Get(query);

                return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
            }
            catch
            {
                return null;
            }
        }
        #endregion GET

        public bool Update<TModel>(IEnumerable<TModel> cars)
        {
            try
            {
                foreach (var car in cars)
                {
                    var model = Mapper.Map<TModel, TEntity>(car);
                    var updateResult = DataRepository.Update((int)model.Id, model);

                    if (!updateResult) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update<TModel>(int id, TModel dto)
        {
            try
            {
                var model = Mapper.Map<TModel, TEntity>(dto);
                return DataRepository.Update(id, model);
            }
            catch
            {
                return false;
            }
        }
    }
}
