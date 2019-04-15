export interface Error {
  error: {
    code: string;
    message: string;
  };
}

export interface Order {
  Id: number;
  UserName: string;
  OrderDate: string;
}

export interface OrderDetail {
  Id: number;
  Quantity: number;
  OrderId: number;
  ProductId: number;
}

export interface Product {
  Id: number;
  ProductName: string;
  ProductPrice: number;
  ProductPicture: string;
}
