using System;
using Microsoft.AspNetCore.Mvc;

namespace CustomSecurity.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
