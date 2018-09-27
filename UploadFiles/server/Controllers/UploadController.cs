using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Test
{
    [Route("upload")]
    public partial class UploadController : Controller
    {
        [HttpPost]
        public ActionResult Post(ICollection<IFormFile> files)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}