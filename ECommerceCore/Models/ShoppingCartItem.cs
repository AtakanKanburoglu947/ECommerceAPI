﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")] 
        
        public Product? Product { get; set; }
    }
}
