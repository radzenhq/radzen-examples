export interface Error {
  error: {
    code: string;
    message: string;
  };
}

export interface Order {
  Id: number;
  OrderDate: string;
  OrderDetails: Array<OrderDetail>;
  UserName: string;
}

export interface OrderDetail {
  Id: number;
  Order: Order;
  OrderId: number;
  Product: Product;
  ProductId: number;
  Quantity: number;
}

export interface Product {
  Id: number;
  OrderDetails: Array<OrderDetail>;
  ProductName: string;
  ProductPicture: string;
  ProductPrice: number;
}
