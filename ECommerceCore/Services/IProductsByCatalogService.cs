using ECommerceCore.Models;
using ECommerceCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IProductsByCatalogService
    {
        List<ProductVM> GetProductsByCatalogName(string catalogName);
    }
}
