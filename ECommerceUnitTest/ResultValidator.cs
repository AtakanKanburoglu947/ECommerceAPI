using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest
{
    internal static class ResultValidator
    {
        internal static void ValidateResult(IActionResult result)
        {
            OkObjectResult? okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.IsNotNull(okObjectResult.Value);
        }
    }
}
