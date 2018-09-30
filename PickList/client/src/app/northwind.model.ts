export interface Category {
  CategoryID: number;
  CategoryName: string;
  Description: string;
  Picture: string;
  Products: Array<Product>;
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
  EmployeeTerritories: Array<EmployeeTerritory>;
  Employees: Array<Employee>;
}

export interface EmployeeTerritory {
  EmployeeID: number;
  TerritoryID: string;
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

export interface Region {
  RegionID: number;
  RegionDescription: string;
  Territories: Array<Territory>;
}

export interface Shipper {
  ShipperID: number;
  CompanyName: string;
  Phone: string;
  Orders: Array<Order>;
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
