import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  serverUrl = environment.apiServer

  constructor(private http: HttpClient) { }

  getRestaurants() : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/GetRestaurants`;
    return this.http.get(url);
  }

  getRestaurantMenu(id: string) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/GetRestaurantMenuItems?restaurantId=${id}`;
    return this.http.get(url);
  }
}
