using System;
using Microsoft.AspNetCore.Mvc;

namespace AuditTrail.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
