export interface Category {
  CategoryID: number;
  CategoryName: string;
  Description: string;
  Picture: string;
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
}

export interface CustomerCustomerDemo {
  CustomerID: string;
  CustomerTypeID: string;
}

export interface CustomerDemographic {
  CustomerTypeID: string;
  CustomerDesc: string;
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
}

export interface Region {
  RegionID: number;
  RegionDescription: string;
}

export interface Shipper {
  ShipperID: number;
  CompanyName: string;
  Phone: string;
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
}

export interface Territory {
  TerritoryID: string;
  TerritoryDescription: string;
  RegionID: number;
}
