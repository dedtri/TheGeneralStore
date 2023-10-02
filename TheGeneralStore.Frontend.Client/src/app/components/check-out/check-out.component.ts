import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/models/order.model';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.css']
})
export class CheckOutComponent {
  cartItems: any[] = []; 
  userCredentials: any;
  totalCost: number = 0;
  order: any;

  constructor(private orderService: OrderService, private productService: ProductService, private router: Router) {
    const cartItemsJson = localStorage.getItem('localCart');
    if (cartItemsJson) {
      this.cartItems = JSON.parse(cartItemsJson);
    } else {
      this.cartItems = []; 
    }

    const userCredentialsJson = localStorage.getItem('credentials');
    if (userCredentialsJson) {
      this.userCredentials = JSON.parse(userCredentialsJson);
    } else {
      this.userCredentials = {}; 
    }

    this.updateTotalCost();

    this.order = {
      id: 0,
      date: new Date(), // You can set the order date as needed
      total_price: this.totalCost,
      orderProducts: this.cartItems.map(cartItem => ({
        productId: cartItem.id,
        name: cartItem.name,
        quantity: cartItem.quantity,
        price: cartItem.price
      })),
      customerId: this.userCredentials.id // Assign the customer ID from user credentials
    };

   }

   updateTotalCost() {
    this.totalCost = this.cartItems.reduce((acc, item) => {
      return acc + (item.price * item.quantity);
    }, 0);
  }

   confirmOrder() {
    console.log(this.order)
    this.orderService.add(this.order).subscribe({
      next: (resp: any) => {
        console.log(resp);
        this.productService.clearCart();
        this.router.navigateByUrl("/thankyou");
      },
      error: (err) => {
        console.log(err);
      }
    })
   }
}
