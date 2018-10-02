using System;
using Microsoft.AspNetCore.Mvc;

namespace ExtendApplicationUser.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
