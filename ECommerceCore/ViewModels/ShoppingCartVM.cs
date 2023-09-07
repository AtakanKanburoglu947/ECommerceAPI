using ECommerceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<Product>? Products { get; set; }
    }
}
