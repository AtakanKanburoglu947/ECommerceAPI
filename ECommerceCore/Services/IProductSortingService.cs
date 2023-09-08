using ECommerceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IProductSortingService
    {
        IEnumerable<Product> SortByPrice(string query);
    }
}
