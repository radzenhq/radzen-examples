using System;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenantSample.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
