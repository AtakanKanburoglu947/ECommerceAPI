using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class VMMapper : Profile
    {
        public VMMapper() { 
            CreateMap<UserVM,User>().ReverseMap();
            CreateMap<ProductVM,Product>().ReverseMap();
            CreateMap<SellerVM,Seller>().ReverseMap();
            CreateMap<CatalogVM,Catalog>().ReverseMap();
        }

    }
}
