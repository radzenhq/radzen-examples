export interface AlphabeticalListOfProduct {
  ProductID: number;
  ProductName: string;
  SupplierID: number;
  CategoryID: number;
  QuantityPerUnit: string;
  UnitPrice: number;
  UnitsInStock: number;
  UnitsOnOrder: number;
  ReorderLevel: number;
  Discontinued: boolean;
  CategoryName: string;
}

export interface Category {
  CategoryID: number;
  CategoryName: string;
  Description: string;
  Picture: string;
  Products: Array<Product>;
}

export interface CategorySalesFor1997 {
  CategoryName: string;
  CategorySales: number;
}

export interface CurrentProductList {
  ProductID: number;
  ProductName: string;
}

export interface Customer {
  CustomerID: string;
  CompanyName: string;
  ContactName: string;
  ContactTitle: string;
  Address: string;
  City: string;
  Region: string;
  PostalCode: string;
  Country: string;
  Phone: string;
  Fax: string;
  Orders: Array<Order>;
  CustomerCustomerDemos: Array<CustomerCustomerDemo>;
}

export interface CustomerAndSuppliersByCity {
  City: string;
  CompanyName: string;
  ContactName: string;
  Relationship: string;
}

export interface CustomerCustomerDemo {
  CustomerID: string;
  CustomerTypeID: string;
}

export interface CustomerDemographic {
  CustomerTypeID: string;
  CustomerDesc: string;
  CustomerCustomerDemos: Array<CustomerCustomerDemo>;
}

export interface Employee {
  EmployeeID: number;
  LastName: string;
  FirstName: string;
  Title: string;
  TitleOfCourtesy: string;
  BirthDate: string;
  HireDate: string;
  Address: string;
  City: string;
  Region: string;
  PostalCode: string;
  Country: string;
  HomePhone: string;
  Extension: string;
  Photo: string;
  Notes: string;
  ReportsTo: number;
  PhotoPath: string;
  Orders: Array<Order>;
  Employees: Array<Employee>;
  EmployeeTerritories: Array<EmployeeTerritory>;
}

export interface EmployeeTerritory {
  EmployeeID: number;
  TerritoryID: string;
}

export interface Invoice {
  ShipName: string;
  ShipAddress: string;
  ShipCity: string;
  ShipRegion: string;
  ShipPostalCode: string;
  ShipCountry: string;
  CustomerID: string;
  CustomerName: string;
  Address: string;
  City: string;
  Region: string;
  PostalCode: string;
  Country: string;
  Salesperson: string;
  OrderID: number;
  OrderDate: string;
  RequiredDate: string;
  ShippedDate: string;
  ShipperName: string;
  ProductID: number;
  ProductName: string;
  UnitPrice: number;
  Quantity: number;
  Discount: number;
  ExtendedPrice: number;
  Freight: number;
}

export interface Order {
  OrderID: number;
  CustomerID: string;
  EmployeeID: number;
  OrderDate: string;
  RequiredDate: string;
  ShippedDate: string;
  ShipVia: number;
  Freight: number;
  ShipName: string;
  ShipAddress: string;
  ShipCity: string;
  ShipRegion: string;
  ShipPostalCode: string;
  ShipCountry: string;
  OrderDetails: Array<OrderDetail>;
}

export interface OrderDetail {
  OrderID: number;
  ProductID: number;
  UnitPrice: number;
  Quantity: number;
  Discount: number;
}

export interface OrderDetailsExtended {
  OrderID: number;
  ProductID: number;
  ProductName: string;
  UnitPrice: number;
  Quantity: number;
  Discount: number;
  ExtendedPrice: number;
}

export interface OrderSubtotal {
  OrderID: number;
  Subtotal: number;
}

export interface OrdersQry {
  OrderID: number;
  CustomerID: string;
  EmployeeID: number;
  OrderDate: string;
  RequiredDate: string;
  ShippedDate: string;
  ShipVia: number;
  Freight: number;
  ShipName: string;
  ShipAddress: string;
  ShipCity: string;
  ShipRegion: string;
  ShipPostalCode: string;
  ShipCountry: string;
  CompanyName: string;
  Address: string;
  City: string;
  Region: string;
  PostalCode: string;
  Country: string;
}

export interface Product {
  ProductID: number;
  ProductName: string;
  SupplierID: number;
  CategoryID: number;
  QuantityPerUnit: string;
  UnitPrice: number;
  UnitsInStock: number;
  UnitsOnOrder: number;
  ReorderLevel: number;
  Discontinued: boolean;
  OrderDetails: Array<OrderDetail>;
}

export interface ProductSalesFor1997 {
  CategoryName: string;
  ProductName: string;
  ProductSales: number;
}

export interface ProductsAboveAveragePrice {
  ProductName: string;
  UnitPrice: number;
}

export interface ProductsByCategory {
  CategoryName: string;
  ProductName: string;
  QuantityPerUnit: string;
  UnitsInStock: number;
  Discontinued: boolean;
}

export interface QuarterlyOrder {
  CustomerID: string;
  CompanyName: string;
  City: string;
  Country: string;
}

export interface Region {
  RegionID: number;
  RegionDescription: string;
  Territories: Array<Territory>;
}

export interface SalesByCategory {
  CategoryID: number;
  CategoryName: string;
  ProductName: string;
  ProductSales: number;
}

export interface SalesTotalsByAmount {
  SaleAmount: number;
  OrderID: number;
  CompanyName: string;
  ShippedDate: string;
}

export interface Shipper {
  ShipperID: number;
  CompanyName: string;
  Phone: string;
  Orders: Array<Order>;
}

export interface SummaryOfSalesByQuarter {
  ShippedDate: string;
  OrderID: number;
  Subtotal: number;
}

export interface SummaryOfSalesByYear {
  ShippedDate: string;
  OrderID: number;
  Subtotal: number;
}

export interface Supplier {
  SupplierID: number;
  CompanyName: string;
  ContactName: string;
  ContactTitle: string;
  Address: string;
  City: string;
  Region: string;
  PostalCode: string;
  Country: string;
  Phone: string;
  Fax: string;
  HomePage: string;
  Products: Array<Product>;
}

export interface Territory {
  TerritoryID: string;
  TerritoryDescription: string;
  RegionID: number;
  EmployeeTerritories: Array<EmployeeTerritory>;
}
