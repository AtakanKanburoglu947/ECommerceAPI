﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.ViewModels
{
    public class ProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int SellerId { get; set; }
        public int CatalogId { get; set; }
    }
}
