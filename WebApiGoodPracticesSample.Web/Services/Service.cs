using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Services
{
    public class Service<TModel> : IService<TModel> where TModel : CommonEntity
    {
        private readonly IMapper _mapper;
        protected readonly IDataRepository<TModel> _dataRepository;

        public Service(IMapper mapper, IDataRepository<TModel> dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        public bool Create<TDto>(TDto dto)
        {
            try
            {
                var model = _mapper.Map<TDto, TModel>(dto);

                return _dataRepository.Create(model);
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return _dataRepository.Delete(id);
            }
            catch
            {
                return false;
            }
        }

        public virtual TModel Get(int id)
        {
            try
            {
                return Get(new List<int> { id }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<TModel> Get(IEnumerable<int> ids)
        {
            try
            {
                return _dataRepository.Get(ids);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<TModel> Get(Expression<Func<TModel, bool>> query)
        {
            try
            {
                return _dataRepository.Get(query);
            }
            catch
            {
                return null;
            }
        }

        public bool Update<TDto>(IEnumerable<TDto> cars)
        {
            try
            {
                foreach (var car in cars)
                {
                    var model = _mapper.Map<TDto, TModel>(car);
                    var updateResult = _dataRepository.Update(model.Id, model);

                    if (!updateResult) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update<TDto>(int id, TDto dto)
        {
            try
            {
                var model = _mapper.Map<TDto, TModel>(dto);
                return _dataRepository.Update(id, model);
            }
            catch
            {
                return false;
            }
        }
    }
}
