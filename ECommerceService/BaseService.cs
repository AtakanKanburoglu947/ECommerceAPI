using AutoMapper;
using ECommerceCore.Repositories;
using ECommerceCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class BaseService<T,VM> : IBaseService<T,VM> where T : class where VM : class 
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<T> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VM> Add(VM entity)
        {
            await _repository.Add(_mapper.Map<T>(entity));
            return entity;
        }

        public Task Delete(VM entity)
        {
             _repository.Delete(_mapper.Map<T>(entity));
             return Task.CompletedTask;
        }

        public async Task<IEnumerable<VM>> GetAll()
        {
            return _mapper.Map<IEnumerable<VM>>(await _repository.GetAll());
        }

        public async Task<VM> GetById(int id)
        {
           return _mapper.Map<VM>(await _repository.GetById(id));
        }

        public Task<VM> Update(VM entity)
        {
            T updatedEntity = _mapper.Map<T>(entity);
            _repository.Update(updatedEntity);
            return Task.FromResult(entity);
        }
    }
}
