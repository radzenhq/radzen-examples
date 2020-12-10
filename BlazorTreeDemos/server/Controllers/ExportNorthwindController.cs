using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeDemos.Data;

namespace TreeDemos
{
    public partial class ExportNorthwindController : ExportController
    {
        private readonly NorthwindContext context;

        public ExportNorthwindController(NorthwindContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/Northwind/categories/csv")]
        [HttpGet("/export/Northwind/categories/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Categories, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/categories/excel")]
        [HttpGet("/export/Northwind/categories/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Categories, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/customers/csv")]
        [HttpGet("/export/Northwind/customers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCustomersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Customers, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/customers/excel")]
        [HttpGet("/export/Northwind/customers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCustomersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Customers, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/customercustomerdemos/csv")]
        [HttpGet("/export/Northwind/customercustomerdemos/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCustomerCustomerDemosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.CustomerCustomerDemos, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/customercustomerdemos/excel")]
        [HttpGet("/export/Northwind/customercustomerdemos/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCustomerCustomerDemosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.CustomerCustomerDemos, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/customerdemographics/csv")]
        [HttpGet("/export/Northwind/customerdemographics/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCustomerDemographicsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.CustomerDemographics, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/customerdemographics/excel")]
        [HttpGet("/export/Northwind/customerdemographics/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCustomerDemographicsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.CustomerDemographics, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/employees/csv")]
        [HttpGet("/export/Northwind/employees/csv(fileName='{fileName}')")]
        public FileStreamResult ExportEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Employees, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/employees/excel")]
        [HttpGet("/export/Northwind/employees/excel(fileName='{fileName}')")]
        public FileStreamResult ExportEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Employees, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/employeeterritories/csv")]
        [HttpGet("/export/Northwind/employeeterritories/csv(fileName='{fileName}')")]
        public FileStreamResult ExportEmployeeTerritoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.EmployeeTerritories, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/employeeterritories/excel")]
        [HttpGet("/export/Northwind/employeeterritories/excel(fileName='{fileName}')")]
        public FileStreamResult ExportEmployeeTerritoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.EmployeeTerritories, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/orders/csv")]
        [HttpGet("/export/Northwind/orders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/orders/excel")]
        [HttpGet("/export/Northwind/orders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/orderdetails/csv")]
        [HttpGet("/export/Northwind/orderdetails/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/orderdetails/excel")]
        [HttpGet("/export/Northwind/orderdetails/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/products/csv")]
        [HttpGet("/export/Northwind/products/csv(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/products/excel")]
        [HttpGet("/export/Northwind/products/excel(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/regions/csv")]
        [HttpGet("/export/Northwind/regions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRegionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Regions, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/regions/excel")]
        [HttpGet("/export/Northwind/regions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRegionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Regions, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/shippers/csv")]
        [HttpGet("/export/Northwind/shippers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportShippersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Shippers, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/shippers/excel")]
        [HttpGet("/export/Northwind/shippers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportShippersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Shippers, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/suppliers/csv")]
        [HttpGet("/export/Northwind/suppliers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSuppliersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Suppliers, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/suppliers/excel")]
        [HttpGet("/export/Northwind/suppliers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSuppliersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Suppliers, Request.Query), fileName);
        }
        [HttpGet("/export/Northwind/territories/csv")]
        [HttpGet("/export/Northwind/territories/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTerritoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Territories, Request.Query), fileName);
        }

        [HttpGet("/export/Northwind/territories/excel")]
        [HttpGet("/export/Northwind/territories/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTerritoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Territories, Request.Query), fileName);
        }
    }
}
