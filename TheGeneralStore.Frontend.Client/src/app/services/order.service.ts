import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Image } from '../models/image.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { queryResult } from '../models/queryResult.model';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private readonly url = environment.serverURL + 'Orders/';
  public readonly imageUrl = environment.imageURL;

  constructor(private http: HttpClient) { }

  add(order: any) {
    return this.http.post<any>(this.url, order)
  }

  get(id: string | number): Observable<Order> {
    return this.http.get<Order>(this.url + id);
  }

  getAll(filter: any) {
    return this.http.get<queryResult>(this.url + '?' + this.toQueryString(filter));
  }

  delete(id: number) {
    return this.http.delete<Order>(this.url + "delete/" + id);
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
