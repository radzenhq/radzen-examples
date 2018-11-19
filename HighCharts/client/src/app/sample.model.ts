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
  OrderDetails: Array<OrderDetail>;
}

export interface OrderDetail {
  Id: number;
  Quantity: number;
  OrderId: number;
  ProductId: number;
  Order: Order;
  Product: Product;
}

export interface Product {
  Id: number;
  ProductName: string;
  ProductPrice: number;
  ProductPicture: string;
  OrderDetails: Array<OrderDetail>;
}
