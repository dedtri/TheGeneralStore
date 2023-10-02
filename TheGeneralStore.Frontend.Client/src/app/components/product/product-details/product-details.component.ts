import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {

  product: any;
  categories: any;
  imageUrl = environment.imageURL;
  removeCart = false;

  constructor(private route: ActivatedRoute, private productService: ProductService, private location: Location, private categoryService: CategoryService) {

  this.initialize();
  this.initializeCategoryNames();

  }

  initialize() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.productService.get(id).subscribe({
            next: (resp) => {
              this.product = resp;
              let cartData = localStorage.getItem('localCart');
              if (id && cartData)
              {
                let items = JSON.parse(cartData);
                items = items.filter((item: Product) => id === item.id.toString());
                if(items.length) {
                  this.removeCart = true;
                }
              }
              console.log(this.product);
            },
            error: (err) => {
              console.log(err);
            }
          });
        }
      }
    })
  }

  initializeCategoryNames() {
    this.categoryService.getAll().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: (err) => {
        console.log(err)
      }
    })
  }

  addToCart() {
    this.product.quantity = 1;
    this.productService.addToCart(this.product);
    this.removeCart = true;
  }

  removeFromCart(productid: any) {
    this.productService.removeFromCart(productid);
    this.removeCart = false;
  }

  back() {
    this.location.back();
  }
}


