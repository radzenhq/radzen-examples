using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Results;
using MyApp.Models.Test;

namespace MyApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        private Data.TestContext context;

        public ServerMethodsController(Data.TestContext context)
        {
        this.context = context;
        }

        [HttpPost]
        public IActionResult UpdateProduct(int key, string patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var itemToUpdate = this.context.Products.Where(i => i.Id == key).FirstOrDefault();

                if (itemToUpdate == null)
                {
                    ModelState.AddModelError("", "Item no longer available");
                    return BadRequest(ModelState);
                }

                var newItem = System.Text.Json.JsonSerializer.Deserialize<Product>(patch);

                itemToUpdate.ProductName = newItem.ProductName;
                itemToUpdate.ProductPictureAsString = newItem.ProductPictureAsString;

                this.context.Products.Update(itemToUpdate);
                this.context.SaveChanges();

                var itemToReturn = this.context.Products.Where(i => i.Id == key);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
