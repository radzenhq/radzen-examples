using System;
using Microsoft.AspNetCore.Mvc;

namespace TreeGrid.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
