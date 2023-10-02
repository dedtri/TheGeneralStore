import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment';
import { queryResult } from '../models/queryResult.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly url = environment.serverURL + 'Categories/';

  constructor(private http: HttpClient) { }

  add(category: Category) {
    console.log(this.url);
    return this.http.post<Category>(this.url, category)
  }

  get(id: number): Observable<Category> {
    return this.http.get<Category>(this.url + id);
  }

  getByName(name: string): Observable<Category> {
    return this.http.get<Category>(this.url + 'name/' + name);
  }

  getAll() {
    return this.http.get<queryResult>(this.url);
  }

  delete(id: number) {
    return this.http.delete<Category>(this.url + "delete/" + id);
  }

  update(id: number, updateRequest: Category) {
    return this.http.put<Category>(this.url + id, updateRequest);
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