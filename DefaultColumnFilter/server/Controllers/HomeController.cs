using System;
using Microsoft.AspNetCore.Mvc;

namespace DefaultColumnFilter.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
