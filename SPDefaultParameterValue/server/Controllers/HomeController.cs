using System;
using Microsoft.AspNetCore.Mvc;

namespace SpDefaultParameterValue.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
