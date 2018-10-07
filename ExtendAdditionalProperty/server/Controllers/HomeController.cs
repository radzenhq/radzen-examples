using System;
using Microsoft.AspNetCore.Mvc;

namespace ExtendAdditionalProperty.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
