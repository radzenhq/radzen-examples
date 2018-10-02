using System;
using Microsoft.AspNetCore.Mvc;

namespace GetCurrentUser.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
