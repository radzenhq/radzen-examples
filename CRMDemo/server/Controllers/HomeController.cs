using System;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
