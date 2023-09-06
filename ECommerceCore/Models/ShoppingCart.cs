using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; } 
    }
}
