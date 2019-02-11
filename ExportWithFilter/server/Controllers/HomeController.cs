using System;
using Microsoft.AspNetCore.Mvc;

namespace ExportWithFilter.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
