using ECommerceCore.Models;
using ECommerceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class FilteringService
    {
        private readonly AppDbContext _context;
        public FilteringService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Seller> FilterBySellerName(string sellerName)
        {
            if (!string.IsNullOrEmpty(sellerName))
            {
                return _context.Sellers.Where(x=>x.Name.Contains(sellerName,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return Enumerable.Empty<Seller>();
        }
        public IEnumerable<Product> FilterByProductName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                return _context.Products.Where(x => x.Name.Contains(productName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return Enumerable.Empty<Product>();
        }
        public IEnumerable<Catalog> FilterByCatalogName(string catalogName)
        {
            if (!string.IsNullOrEmpty(catalogName))
            {
                return _context.Catalogs.Where(x => x.Name.Contains(catalogName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return Enumerable.Empty<Catalog>();
        }
    }
}
