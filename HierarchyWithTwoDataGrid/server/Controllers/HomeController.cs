using System;
using Microsoft.AspNetCore.Mvc;

namespace HierarchyWithTwoDataGrid.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
