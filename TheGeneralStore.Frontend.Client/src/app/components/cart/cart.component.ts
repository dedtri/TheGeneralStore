import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {

  imageUrl = environment.imageURL;
  cartData: any[] = [];
  totalCost: number = 0;

  constructor(private productService: ProductService, private router: Router) {
      this.initialize();
  }

  initialize() {
    const cartData = localStorage.getItem('localCart');

    if (cartData) {
      this.cartData = JSON.parse(cartData);
    }
    else {
      this.cartData = [];
    }
  }

  updateTotalCost() {
    this.totalCost = this.cartData.reduce((acc, item) => {
      return acc + (item.price * item.quantity);
    }, 0);
  }

  calculateTotalItems() {
    return this.cartData.reduce((acc, item) => {
      this.updateTotalCost();
      return acc + item.quantity;
    }, 0);
  }

  clearCart() {
    this.productService.clearCart();
    this.initialize();
    this.updateTotalCost();
  }

  checkout() {
    this.router.navigateByUrl('/checkout')
    localStorage.setItem('localCart', JSON.stringify(this.cartData))
  }
}
