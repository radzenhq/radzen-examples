using System;
using Microsoft.AspNetCore.Mvc;

namespace InvokeCustomServerMethod.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
