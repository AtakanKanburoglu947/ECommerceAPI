using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.ViewModels
{
    public class ShoppingCartItemVM
    {
        public int TotalPrice { get; set; } = 0;
        public int Amount { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
