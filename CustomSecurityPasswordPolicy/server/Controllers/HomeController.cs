using System;
using Microsoft.AspNetCore.Mvc;

namespace CustomSecurityPasswordPolicy.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
