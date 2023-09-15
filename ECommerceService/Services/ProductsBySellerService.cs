using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ProductsBySellerService : IProductsBySellerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductsBySellerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ProductVM> GetAllProductsBySellerName(string sellerName)
        {
            Seller? seller = _context.Sellers.FirstOrDefault(x => x.Name == sellerName);
            if (seller == null)
            {
                throw new Exception("Could not find the seller");
            }
            List<Product> products = _context.Products.Where(x => x.SellerId == seller.Id).ToList();
            return _mapper.Map<List<ProductVM>>(products);
        }
    }
}
