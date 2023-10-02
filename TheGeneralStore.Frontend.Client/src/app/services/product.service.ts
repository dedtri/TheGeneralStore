import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from 'src/environments/environment';
import { queryResult } from '../models/queryResult.model';
import { Cart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly url = environment.serverURL + 'Products/';

  cartData = new EventEmitter<Product[]>;

  constructor(private http: HttpClient) { }

  add(project: Product) {
    return this.http.post<Product>(this.url, project)
  }

  get(id: number | string): Observable<Product> {
    return this.http.get<Product>(this.url + id);
  }

  getAll(filter: any) {
    return this.http.get<queryResult>(this.url + '?' + this.toQueryString(filter));
  }

  delete(id: number) {
    return this.http.delete<Product>(this.url + "delete/" + id);
  }

  update(id: number, updateRequest: Product) {
    return this.http.put<Product>(this.url + id, updateRequest);
  }

  addToCart(data: Cart) {
    let cartData = [];
    let localCart = localStorage.getItem('localCart');
    if (!localCart)
    {
      localStorage.setItem('localCart', JSON.stringify([data]));
      let cartData = localStorage.getItem('localCart');
      if (cartData) {
        let items: Product[] = JSON.parse(cartData);
        this.cartData.emit(items);
      }
    }
    else {
      cartData = JSON.parse(localCart);
      cartData.push(data);
      localStorage.setItem('localCart', JSON.stringify(cartData));
      this.cartData.emit(cartData);
    }
  }

  removeFromCart(productId: any) {
    let cartData = localStorage.getItem('localCart');
    if (cartData) {
      let items: Product[] = JSON.parse(cartData);
      items = items.filter((item: any) => productId !== item.id);
      localStorage.setItem('localCart', JSON.stringify(items));
      this.cartData.emit(items);
    }
  }

  clearCart() {
    localStorage.setItem('localCart', JSON.stringify([]));
    let cartData = localStorage.getItem('localCart');
    if (cartData) {
      let items: Product[] = JSON.parse(cartData);
      this.cartData.emit(items);
    }
  }

  toQueryString(obj: any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }
}