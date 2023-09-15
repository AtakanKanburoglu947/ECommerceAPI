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
            if (!sellerName.IsNullOrEmpty())
            {
                try
                {
                    Seller? seller = _context.Sellers.FirstOrDefault(x => x.Name == sellerName);
                    List<Product> products = _context.Products.Where(x => x.SellerId == seller.Id).ToList();
                    return _mapper.Map<List<ProductVM>>(products);
                }
                catch (Exception)
                {
                    throw new Exception("Could not find the seller");
                }
            }
            else
            {
                throw new Exception("Input is empty");
            }

          

        }
    }
}
