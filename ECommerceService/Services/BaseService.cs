using AutoMapper;
using ECommerceCore.Repositories;
using ECommerceCore.Services;
using ECommerceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class BaseService<T, VM> : IBaseService<T, VM> where T : class where VM : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<T> Add(VM entity)
        {
            var newEntity = _mapper.Map<T>(entity);
            await _repository.Add(newEntity);
            
            return newEntity;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> expression)
        {
            return await _repository.Filter(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T> Update(T entity)
        {
     
            T updatedEntity = _mapper.Map<T>(entity);
            await _repository.Update(updatedEntity);

            return updatedEntity;
        }
    }
}
