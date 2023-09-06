using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IBaseService<T,VM> where T : class where VM : class
    {
        Task<IEnumerable<VM>> GetAll();
        Task<VM> GetById(int id);
        Task<VM> Add(VM entity);
        Task<VM> Update(VM entity);
        Task Delete(VM entity);
    }
}
