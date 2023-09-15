using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ProductsByCatalogService : IProductsByCatalogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductsByCatalogService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ProductVM> GetProductsByCatalogName(string catalogName)
        {
            Catalog? catalog = _context.Catalogs.FirstOrDefault(x=>x.Name == catalogName);
            if (!catalogName.IsNullOrEmpty())
            {
                try
                {
                    List<Product> products = _context.Products.Where(x => x.CatalogId == catalog.Id).ToList();
                    return _mapper.Map<List<ProductVM>>(products);
                }
                catch (Exception)
                {
                    throw new Exception("Could not find the catalog");
                }
            }
            else
            {
                throw new Exception("Input is empty");
            }

        }
    }
}
