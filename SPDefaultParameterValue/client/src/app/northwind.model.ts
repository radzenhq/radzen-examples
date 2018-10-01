export interface CustOrderHist {
  ProductName: string;
  Total: number;
}

export interface CustOrdersDetail {
  ProductName: string;
  UnitPrice: number;
  Quantity: number;
  Discount: number;
  ExtendedPrice: number;
}

export interface CustOrdersOrder {
  OrderID: number;
  OrderDate: string;
  RequiredDate: string;
  ShippedDate: string;
}

export interface EmployeeSalesByCountry {
  Country: string;
  LastName: string;
  FirstName: string;
  ShippedDate: string;
  OrderID: number;
  SaleAmount: number;
}

export interface SalesByCategory1 {
  ProductName: string;
  TotalPurchase: number;
}

export interface SalesByYear {
  ShippedDate: string;
  OrderID: number;
  Subtotal: number;
  Year: string;
}

export interface TenMostExpensiveProduct {
  TenMostExpensiveProducts: string;
  UnitPrice: number;
}
