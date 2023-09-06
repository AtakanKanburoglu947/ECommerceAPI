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
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<VM> Add(VM entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(VM entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VM>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<VM> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VM> Update(VM entity)
        {
            throw new NotImplementedException();
        }
    }
}
