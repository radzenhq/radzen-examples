using System;
using Microsoft.AspNetCore.Mvc;

namespace PickList.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
