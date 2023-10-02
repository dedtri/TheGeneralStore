import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Image } from '../models/image.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { queryResult } from '../models/queryResult.model';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private readonly url = environment.serverURL + 'Images/';
  public readonly imageUrl = environment.imageURL;

  constructor(private http: HttpClient) { }

  add(image: any) {
    let formData = new FormData();
    formData.append("imageFile", image.imageFile);
    formData.append("ProductId", image.ProductId);
    return this.http.post<any>(this.url, formData)
  }

  get(id: string | number): Observable<Image> {
    return this.http.get<Image>(this.url + id);
  }

  getAll(filter: any) {
    return this.http.get<queryResult>(this.url + '?' + this.toQueryString(filter));
  }

  delete(id: number) {
    return this.http.delete<Image>(this.url + "delete/" + id);
  }

  update(id: number) {
    return this.http.put<Image>(this.url + id, Image);
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
