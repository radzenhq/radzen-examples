using Microsoft.AspNetCore.Mvc;
using BlazorMultiTenant.Data;

namespace BlazorMultiTenant
{
    public partial class ExportSampleController : ExportController
    {
        private readonly SampleContext context;

        public ExportSampleController(SampleContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/Sample/orders/csv")]
        public FileStreamResult ExportOrdersToCSV()
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query));
        }

        [HttpGet("/export/Sample/orders/excel")]
        public FileStreamResult ExportOrdersToExcel()
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query));
        }

        [HttpGet("/export/Sample/orderdetails/csv")]
        public FileStreamResult ExportOrderDetailsToCSV()
        {
            return ToCSV(ApplyQuery(context.OrderDetails, Request.Query));
        }

        [HttpGet("/export/Sample/orderdetails/excel")]
        public FileStreamResult ExportOrderDetailsToExcel()
        {
            return ToExcel(ApplyQuery(context.OrderDetails, Request.Query));
        }

        [HttpGet("/export/Sample/products/csv")]
        public FileStreamResult ExportProductsToCSV()
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query));
        }

        [HttpGet("/export/Sample/products/excel")]
        public FileStreamResult ExportProductsToExcel()
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query));
        }
    }
}
