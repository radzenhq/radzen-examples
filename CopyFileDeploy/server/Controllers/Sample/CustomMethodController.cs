using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomMethodController : Controller
    {
        [HttpGet]
        public IActionResult GetFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                return File(System.IO.File.ReadAllBytes(fileName), contentType: "application/pdf");
            }
            return NotFound();
        }
    }
}