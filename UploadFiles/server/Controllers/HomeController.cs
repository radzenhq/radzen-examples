using System;
using Microsoft.AspNetCore.Mvc;

namespace UploadFiles.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
