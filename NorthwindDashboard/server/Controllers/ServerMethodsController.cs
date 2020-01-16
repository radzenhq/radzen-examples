using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        // Sample method which returns the sum of its arguments
        // For more examples check https://www.radzen.com/documentation/invoke-custom-method/
        // public IActionResult Sum(int x, int y)
        // {
        //    var sum = x + y;
        //
        //    return Json(new { sum });
        // }
    }
}
