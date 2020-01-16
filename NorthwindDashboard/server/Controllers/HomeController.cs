using System;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
