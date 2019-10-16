using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NgSelectDataGridCheckBox.Data;

namespace NgSelectDataGridCheckBox.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        [HttpPost]
        public IActionResult DoSomething([FromBody] int[] selectedIds)
        {
            var context = (SampleContext)HttpContext.RequestServices.GetService(typeof(SampleContext));

            var orders =  context.Orders.Where(o => selectedIds.Contains(o.Id));
            
            return new NoContentResult();
        }
    }
}
