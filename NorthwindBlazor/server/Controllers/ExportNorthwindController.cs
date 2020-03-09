using Microsoft.AspNetCore.Mvc;
using NorthwindBlazor.Data;

namespace NorthwindBlazor
{
    public partial class ExportNorthwindController : ExportController
    {
        private readonly NorthwindContext context;

        public ExportNorthwindController(NorthwindContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/Northwind/alphabeticallistofproducts/csv")]
        public FileStreamResult ExportAlphabeticalListOfProductsToCSV()
        {
            return ToCSV(ApplyQuery(context.AlphabeticalListOfProducts, Request.Query));
        }

        [HttpGet("/export/Northwind/alphabeticallistofproducts/excel")]
        public FileStreamResult ExportAlphabeticalListOfProductsToExcel()
        {
            return ToExcel(ApplyQuery(context.AlphabeticalListOfProducts, Request.Query));
        }

        [HttpGet("/export/Northwind/categories/csv")]
        public FileStreamResult ExportCategoriesToCSV()
        {
            return ToCSV(ApplyQuery(context.Categories, Request.Query));
        }

        [HttpGet("/export/Northwind/categories/excel")]
        public FileStreamResult ExportCategoriesToExcel()
        {
            return ToExcel(ApplyQuery(context.Categories, Request.Query));
        }

        [HttpGet("/export/Northwind/categorysalesfor1997s/csv")]
        public FileStreamResult ExportCategorySalesFor1997sToCSV()
        {
            return ToCSV(ApplyQuery(context.CategorySalesFor1997s, Request.Query));
        }

        [HttpGet("/export/Northwind/categorysalesfor1997s/excel")]
        public FileStreamResult ExportCategorySalesFor1997sToExcel()
        {
            return ToExcel(ApplyQuery(context.CategorySalesFor1997s, Request.Query));
        }

        [HttpGet("/export/Northwind/currentproductlists/csv")]
        public FileStreamResult ExportCurrentProductListsToCSV()
        {
            return ToCSV(ApplyQuery(context.CurrentProductLists, Request.Query));
        }

        [HttpGet("/export/Northwind/currentproductlists/excel")]
        public FileStreamResult ExportCurrentProductListsToExcel()
        {
            return ToExcel(ApplyQuery(context.CurrentProductLists, Request.Query));
        }

        [HttpGet("/export/Northwind/custorderhists/csv")]
        public FileStreamResult ExportCustOrderHistsToCSV()
        {
            return ToCSV(ApplyQuery(context.CustOrderHists, Request.Query));
        }

        [HttpGet("/export/Northwind/custorderhists/excel")]
        public FileStreamResult ExportCustOrderHistsToExcel()
        {
            return ToExcel(ApplyQuery(context.CustOrderHists, Request.Query));
        }

        [HttpGet("/export/Northwind/custordersdetails/csv")]
        public FileStreamResult ExportCustOrdersDetailsToCSV()
        {
            return ToCSV(ApplyQuery(context.CustOrdersDetails, Request.Query));
        }

        [HttpGet("/export/Northwind/custordersdetails/excel")]
        public FileStreamResult ExportCustOrdersDetailsToExcel()
        {
            return ToExcel(ApplyQuery(context.CustOrdersDetails, Request.Query));
        }

        [HttpGet("/export/Northwind/custordersorders/csv")]
        public FileStreamResult ExportCustOrdersOrdersToCSV()
        {
            return ToCSV(ApplyQuery(context.CustOrdersOrders, Request.Query));
        }

        [HttpGet("/export/Northwind/custordersorders/excel")]
        public FileStreamResult ExportCustOrdersOrdersToExcel()
        {
            return ToExcel(ApplyQuery(context.CustOrdersOrders, Request.Query));
        }

        [HttpGet("/export/Northwind/customers/csv")]
        public FileStreamResult ExportCustomersToCSV()
        {
            return ToCSV(ApplyQuery(context.Customers, Request.Query));
        }

        [HttpGet("/export/Northwind/customers/excel")]
        public FileStreamResult ExportCustomersToExcel()
        {
            return ToExcel(ApplyQuery(context.Customers, Request.Query));
        }

        [HttpGet("/export/Northwind/customerandsuppliersbycities/csv")]
        public FileStreamResult ExportCustomerAndSuppliersByCitiesToCSV()
        {
            return ToCSV(ApplyQuery(context.CustomerAndSuppliersByCities, Request.Query));
        }

        [HttpGet("/export/Northwind/customerandsuppliersbycities/excel")]
        public FileStreamResult ExportCustomerAndSuppliersByCitiesToExcel()
        {
            return ToExcel(ApplyQuery(context.CustomerAndSuppliersByCities, Request.Query));
        }

        [HttpGet("/export/Northwind/customercustomerdemos/csv")]
        public FileStreamResult ExportCustomerCustomerDemosToCSV()
        {
            return ToCSV(ApplyQuery(context.CustomerCustomerDemos, Request.Query));
        }

        [HttpGet("/export/Northwind/customercustomerdemos/excel")]
        public FileStreamResult ExportCustomerCustomerDemosToExcel()
        {
            return ToExcel(ApplyQuery(context.CustomerCustomerDemos, Request.Query));
        }

        [HttpGet("/export/Northwind/customerdemographics/csv")]
        public FileStreamResult ExportCustomerDemographicsToCSV()
        {
            return ToCSV(ApplyQuery(context.CustomerDemographics, Request.Query));
        }

        [HttpGet("/export/Northwind/customerdemographics/excel")]
        public FileStreamResult ExportCustomerDemographicsToExcel()
        {
            return ToExcel(ApplyQuery(context.CustomerDemographics, Request.Query));
        }

        [HttpGet("/export/Northwind/employees/csv")]
        public FileStreamResult ExportEmployeesToCSV()
        {
            return ToCSV(ApplyQuery(context.Employees, Request.Query));
        }

        [HttpGet("/export/Northwind/employees/excel")]
        public FileStreamResult ExportEmployeesToExcel()
        {
            return ToExcel(ApplyQuery(context.Employees, Request.Query));
        }

        [HttpGet("/export/Northwind/employeesalesbycountries/csv")]
        public FileStreamResult ExportEmployeeSalesByCountriesToCSV()
        {
            return ToCSV(ApplyQuery(context.EmployeeSalesByCountries, Request.Query));
        }

        [HttpGet("/export/Northwind/employeesalesbycountries/excel")]
        public FileStreamResult ExportEmployeeSalesByCountriesToExcel()
        {
            return ToExcel(ApplyQuery(context.EmployeeSalesByCountries, Request.Query));
        }

        [HttpGet("/export/Northwind/employeeterritories/csv")]
        public FileStreamResult ExportEmployeeTerritoriesToCSV()
        {
            return ToCSV(ApplyQuery(context.EmployeeTerritories, Request.Query));
        }

        [HttpGet("/export/Northwind/employeeterritories/excel")]
        public FileStreamResult ExportEmployeeTerritoriesToExcel()
        {
            return ToExcel(ApplyQuery(context.EmployeeTerritories, Request.Query));
        }

        [HttpGet("/export/Northwind/invoices/csv")]
        public FileStreamResult ExportInvoicesToCSV()
        {
            return ToCSV(ApplyQuery(context.Invoices, Request.Query));
        }

        [HttpGet("/export/Northwind/invoices/excel")]
        public FileStreamResult ExportInvoicesToExcel()
        {
            return ToExcel(ApplyQuery(context.Invoices, Request.Query));
        }

        [HttpGet("/export/Northwind/orders/csv")]
        public FileStreamResult ExportOrdersToCSV()
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query));
        }

        [HttpGet("/export/Northwind/orders/excel")]
        public FileStreamResult ExportOrdersToExcel()
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query));
        }

        [HttpGet("/export/Northwind/orderdetails/csv")]
        public FileStreamResult ExportOrderDetailsToCSV()
        {
            return ToCSV(ApplyQuery(context.OrderDetails, Request.Query));
        }

        [HttpGet("/export/Northwind/orderdetails/excel")]
        public FileStreamResult ExportOrderDetailsToExcel()
        {
            return ToExcel(ApplyQuery(context.OrderDetails, Request.Query));
        }

        [HttpGet("/export/Northwind/orderdetailsextendeds/csv")]
        public FileStreamResult ExportOrderDetailsExtendedsToCSV()
        {
            return ToCSV(ApplyQuery(context.OrderDetailsExtendeds, Request.Query));
        }

        [HttpGet("/export/Northwind/orderdetailsextendeds/excel")]
        public FileStreamResult ExportOrderDetailsExtendedsToExcel()
        {
            return ToExcel(ApplyQuery(context.OrderDetailsExtendeds, Request.Query));
        }

        [HttpGet("/export/Northwind/ordersubtotals/csv")]
        public FileStreamResult ExportOrderSubtotalsToCSV()
        {
            return ToCSV(ApplyQuery(context.OrderSubtotals, Request.Query));
        }

        [HttpGet("/export/Northwind/ordersubtotals/excel")]
        public FileStreamResult ExportOrderSubtotalsToExcel()
        {
            return ToExcel(ApplyQuery(context.OrderSubtotals, Request.Query));
        }

        [HttpGet("/export/Northwind/ordersqries/csv")]
        public FileStreamResult ExportOrdersQriesToCSV()
        {
            return ToCSV(ApplyQuery(context.OrdersQries, Request.Query));
        }

        [HttpGet("/export/Northwind/ordersqries/excel")]
        public FileStreamResult ExportOrdersQriesToExcel()
        {
            return ToExcel(ApplyQuery(context.OrdersQries, Request.Query));
        }

        [HttpGet("/export/Northwind/products/csv")]
        public FileStreamResult ExportProductsToCSV()
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query));
        }

        [HttpGet("/export/Northwind/products/excel")]
        public FileStreamResult ExportProductsToExcel()
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query));
        }

        [HttpGet("/export/Northwind/productsalesfor1997s/csv")]
        public FileStreamResult ExportProductSalesFor1997sToCSV()
        {
            return ToCSV(ApplyQuery(context.ProductSalesFor1997s, Request.Query));
        }

        [HttpGet("/export/Northwind/productsalesfor1997s/excel")]
        public FileStreamResult ExportProductSalesFor1997sToExcel()
        {
            return ToExcel(ApplyQuery(context.ProductSalesFor1997s, Request.Query));
        }

        [HttpGet("/export/Northwind/productsaboveaverageprices/csv")]
        public FileStreamResult ExportProductsAboveAveragePricesToCSV()
        {
            return ToCSV(ApplyQuery(context.ProductsAboveAveragePrices, Request.Query));
        }

        [HttpGet("/export/Northwind/productsaboveaverageprices/excel")]
        public FileStreamResult ExportProductsAboveAveragePricesToExcel()
        {
            return ToExcel(ApplyQuery(context.ProductsAboveAveragePrices, Request.Query));
        }

        [HttpGet("/export/Northwind/productsbycategories/csv")]
        public FileStreamResult ExportProductsByCategoriesToCSV()
        {
            return ToCSV(ApplyQuery(context.ProductsByCategories, Request.Query));
        }

        [HttpGet("/export/Northwind/productsbycategories/excel")]
        public FileStreamResult ExportProductsByCategoriesToExcel()
        {
            return ToExcel(ApplyQuery(context.ProductsByCategories, Request.Query));
        }

        [HttpGet("/export/Northwind/quarterlyorders/csv")]
        public FileStreamResult ExportQuarterlyOrdersToCSV()
        {
            return ToCSV(ApplyQuery(context.QuarterlyOrders, Request.Query));
        }

        [HttpGet("/export/Northwind/quarterlyorders/excel")]
        public FileStreamResult ExportQuarterlyOrdersToExcel()
        {
            return ToExcel(ApplyQuery(context.QuarterlyOrders, Request.Query));
        }

        [HttpGet("/export/Northwind/regions/csv")]
        public FileStreamResult ExportRegionsToCSV()
        {
            return ToCSV(ApplyQuery(context.Regions, Request.Query));
        }

        [HttpGet("/export/Northwind/regions/excel")]
        public FileStreamResult ExportRegionsToExcel()
        {
            return ToExcel(ApplyQuery(context.Regions, Request.Query));
        }

        [HttpGet("/export/Northwind/rolepermissions/csv")]
        public FileStreamResult ExportRolePermissionsToCSV()
        {
            return ToCSV(ApplyQuery(context.RolePermissions, Request.Query));
        }

        [HttpGet("/export/Northwind/rolepermissions/excel")]
        public FileStreamResult ExportRolePermissionsToExcel()
        {
            return ToExcel(ApplyQuery(context.RolePermissions, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbycategories/csv")]
        public FileStreamResult ExportSalesByCategoriesToCSV()
        {
            return ToCSV(ApplyQuery(context.SalesByCategories, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbycategories/excel")]
        public FileStreamResult ExportSalesByCategoriesToExcel()
        {
            return ToExcel(ApplyQuery(context.SalesByCategories, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbycategory1s/csv")]
        public FileStreamResult ExportSalesByCategory1sToCSV()
        {
            return ToCSV(ApplyQuery(context.SalesByCategory1s, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbycategory1s/excel")]
        public FileStreamResult ExportSalesByCategory1sToExcel()
        {
            return ToExcel(ApplyQuery(context.SalesByCategory1s, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbyyears/csv")]
        public FileStreamResult ExportSalesByYearsToCSV()
        {
            return ToCSV(ApplyQuery(context.SalesByYears, Request.Query));
        }

        [HttpGet("/export/Northwind/salesbyyears/excel")]
        public FileStreamResult ExportSalesByYearsToExcel()
        {
            return ToExcel(ApplyQuery(context.SalesByYears, Request.Query));
        }

        [HttpGet("/export/Northwind/salestotalsbyamounts/csv")]
        public FileStreamResult ExportSalesTotalsByAmountsToCSV()
        {
            return ToCSV(ApplyQuery(context.SalesTotalsByAmounts, Request.Query));
        }

        [HttpGet("/export/Northwind/salestotalsbyamounts/excel")]
        public FileStreamResult ExportSalesTotalsByAmountsToExcel()
        {
            return ToExcel(ApplyQuery(context.SalesTotalsByAmounts, Request.Query));
        }

        [HttpGet("/export/Northwind/shippers/csv")]
        public FileStreamResult ExportShippersToCSV()
        {
            return ToCSV(ApplyQuery(context.Shippers, Request.Query));
        }

        [HttpGet("/export/Northwind/shippers/excel")]
        public FileStreamResult ExportShippersToExcel()
        {
            return ToExcel(ApplyQuery(context.Shippers, Request.Query));
        }

        [HttpGet("/export/Northwind/summaryofsalesbyquarters/csv")]
        public FileStreamResult ExportSummaryOfSalesByQuartersToCSV()
        {
            return ToCSV(ApplyQuery(context.SummaryOfSalesByQuarters, Request.Query));
        }

        [HttpGet("/export/Northwind/summaryofsalesbyquarters/excel")]
        public FileStreamResult ExportSummaryOfSalesByQuartersToExcel()
        {
            return ToExcel(ApplyQuery(context.SummaryOfSalesByQuarters, Request.Query));
        }

        [HttpGet("/export/Northwind/summaryofsalesbyyears/csv")]
        public FileStreamResult ExportSummaryOfSalesByYearsToCSV()
        {
            return ToCSV(ApplyQuery(context.SummaryOfSalesByYears, Request.Query));
        }

        [HttpGet("/export/Northwind/summaryofsalesbyyears/excel")]
        public FileStreamResult ExportSummaryOfSalesByYearsToExcel()
        {
            return ToExcel(ApplyQuery(context.SummaryOfSalesByYears, Request.Query));
        }

        [HttpGet("/export/Northwind/suppliers/csv")]
        public FileStreamResult ExportSuppliersToCSV()
        {
            return ToCSV(ApplyQuery(context.Suppliers, Request.Query));
        }

        [HttpGet("/export/Northwind/suppliers/excel")]
        public FileStreamResult ExportSuppliersToExcel()
        {
            return ToExcel(ApplyQuery(context.Suppliers, Request.Query));
        }

        [HttpGet("/export/Northwind/tenmostexpensiveproducts/csv")]
        public FileStreamResult ExportTenMostExpensiveProductsToCSV()
        {
            return ToCSV(ApplyQuery(context.TenMostExpensiveProducts, Request.Query));
        }

        [HttpGet("/export/Northwind/tenmostexpensiveproducts/excel")]
        public FileStreamResult ExportTenMostExpensiveProductsToExcel()
        {
            return ToExcel(ApplyQuery(context.TenMostExpensiveProducts, Request.Query));
        }

        [HttpGet("/export/Northwind/territories/csv")]
        public FileStreamResult ExportTerritoriesToCSV()
        {
            return ToCSV(ApplyQuery(context.Territories, Request.Query));
        }

        [HttpGet("/export/Northwind/territories/excel")]
        public FileStreamResult ExportTerritoriesToExcel()
        {
            return ToExcel(ApplyQuery(context.Territories, Request.Query));
        }
    }
}
