using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.ViewModels
{
    public class ErrorVM
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
