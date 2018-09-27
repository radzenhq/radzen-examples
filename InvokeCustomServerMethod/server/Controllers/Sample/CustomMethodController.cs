using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
      public class MyObject
      {
            public string Name { get; set; }
      }

      [Route("api/[controller]/[action]")]
      public class CustomMethodController : Controller
      {
            public IActionResult GetData(string type)
            {
                  var list = new List<MyObject>();

                  for (var i = 0; i < 10; i++)
                  {
                        list.Add(new MyObject() { Name = string.Format("{0}{1}", type, i) });
                  }

                  return Json(list);
            }
      }
}