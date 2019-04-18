using System;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
