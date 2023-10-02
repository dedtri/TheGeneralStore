export interface Order {
  id: number;
  date: Date;
  total_price: number;
  orderProducts: OrderProduct[];
  customerId: number;
}

export interface OrderProduct {
  productId: number;
  quantity: number;
  price: number;
}