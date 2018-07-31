export interface Error {
  error: {
    code: string;
    message: string;
  };
}

export interface Category {
  CategoryID: number;
  CategoryName: string;
  Description: string;
  Picture: string;
  NorthwindProducts: Array<NorthwindProduct>;
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
  NorthwindOrders: Array<NorthwindOrder>;
  CustomerCustomerDemos: Array<CustomerCustomerDemo>;
}

export interface CustomerCustomerDemo {
  CustomerID: string;
  CustomerTypeID: string;
  Customer: Customer;
  CustomerDemographic: CustomerDemographic;
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
  PhotoPath: string;
  NorthwindOrders: Array<NorthwindOrder>;
  EmployeeTerritories: Array<EmployeeTerritory>;
}

export interface EmployeeTerritory {
  EmployeeID: number;
  TerritoryID: string;
  Employee: Employee;
  Territory: Territory;
}

export interface NorthwindOrder {
  OrderID: number;
  CustomerID: string;
  EmployeeID: number;
  OrderDate: string;
  RequiredDate: string;
  ShippedDate: string;
  Freight: number;
  ShipName: string;
  ShipAddress: string;
  ShipCity: string;
  ShipRegion: string;
  ShipPostalCode: string;
  ShipCountry: string;
  NorthwindOrderDetails: Array<NorthwindOrderDetail>;
  Customer: Customer;
  Employee: Employee;
}

export interface NorthwindOrderDetail {
  OrderID: number;
  ProductID: number;
  UnitPrice: number;
  Quantity: number;
  Discount: number;
  NorthwindOrder: NorthwindOrder;
  NorthwindProduct: NorthwindProduct;
}

export interface NorthwindProduct {
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
  NorthwindOrderDetails: Array<NorthwindOrderDetail>;
  Supplier: Supplier;
  Category: Category;
}

export interface Region {
  RegionID: number;
  RegionDescription: string;
  Territories: Array<Territory>;
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
  NorthwindProducts: Array<NorthwindProduct>;
}

export interface Territory {
  TerritoryID: string;
  TerritoryDescription: string;
  RegionID: number;
  EmployeeTerritories: Array<EmployeeTerritory>;
  Region: Region;
}
