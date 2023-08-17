import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly url = environment.serverURL + 'Products/';

  constructor(private http: HttpClient) { }

  add(project: Product) {
    return this.http.post<Product>(this.url, project)
  }

  get(id: number): Observable<Product> {
    return this.http.get<Product>(this.url + id);
  }

  getAll() {
    return this.http.get<Product[]>(this.url);
  }

  delete(id: number) {
    return this.http.delete<Product>(this.url + "delete/" + id);
  }

  update(id: number, updateRequest: Product) {
    return this.http.put<Product>(this.url + id, updateRequest);
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