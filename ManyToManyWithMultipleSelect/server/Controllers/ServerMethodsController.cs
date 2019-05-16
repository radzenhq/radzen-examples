using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.Models.Sample;

namespace Sample.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        private readonly SampleContext context;
        public ServerMethodsController(SampleContext context)
        {
            this.context = context;
        }

        public class SetProductsRequest
        {
            public Product[] Products { get; set; }
            public int OrderID { get; set; }
        }

        [HttpPost]
        public IActionResult UpdateProducts([FromBody]SetProductsRequest request)
        {
            var order = context.Orders
                .Where(o => o.Id == request.OrderID)
                .Include(o => o.OrderDetails)
                .FirstOrDefault();

            if (order != null)
            {
                try
                {
                    // First remove all OrderDetails
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        context.Remove(orderDetail);
                    }
                    // Then add new ones
                    foreach (var product in request.Products)
                    {
                        var orderDetail = new OrderDetail() { OrderId = request.OrderID, ProductId = product.Id };
                        context.Add(orderDetail);
                    }

                    // Persist changes
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = new { message = ex.Message } });
                }
            }

            return NotFound();
        }
    }
}
