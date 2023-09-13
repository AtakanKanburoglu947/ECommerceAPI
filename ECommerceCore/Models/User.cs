using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Models
{
    public class User : IdentityUser
    {
        public float? Balance { get; set; }
    }
}
