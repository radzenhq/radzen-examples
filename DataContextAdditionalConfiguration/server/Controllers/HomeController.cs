using System;
using Microsoft.AspNetCore.Mvc;

namespace DataContextAdditionalConfiguration.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
