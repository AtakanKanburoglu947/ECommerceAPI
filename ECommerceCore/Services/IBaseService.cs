using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IBaseService<T,VM> where T : class where VM : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(VM entity);
        Task<T> Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> expression);

    }
}
