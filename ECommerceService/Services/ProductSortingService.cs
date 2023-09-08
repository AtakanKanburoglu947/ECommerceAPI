using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ProductSortingService : IProductSortingService
    {
        private readonly AppDbContext _context;
        public ProductSortingService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> SortByPrice(string? sortBy){
            IEnumerable<Product> allProducts = _context.Products.OrderBy(x=> x.Price);
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy == "price_desc")
                {
                    allProducts = _context.Products.OrderByDescending(x => x.Price);
                    
                }
            }
            return allProducts;
        }
    }
}
