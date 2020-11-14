import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'
import { RestaurantRequestModel } from '../models/restaurant-model';

@Injectable({
  providedIn: 'root'
})
export class AdminPanelService {

  serverUrl = environment.apiServer

  constructor(private http: HttpClient) { }

  addRestaurant(request: RestaurantRequestModel) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/AddRestaurant`;
    return this.http.post<any>(url, request)
  }

  getAllRestaurants(filter: any) : Observable<any> {
    filter.municipality = filter.municipality === "" ? "" : parseInt(filter.municipality)
    let url = `${this.serverUrl}/api/Restaurants/GetRestaurants?name=${filter.name}&address=${filter.address}&municipality=${filter.municipality}`;
    return this.http.get(url);
  }

  deleteRestaurant(id: string) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/DeleteRestaurant?id=${id}`;
    return this.http.delete(url)
  }

  updateRestaurant(restaurant: any) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/UpdateRestaurant`;
    return this.http.put(url, restaurant);
  }

  updateRestaurantMenu(restaurant: any) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/UpdateRestaurantMenu`;
    return this.http.put(url, restaurant);
  }

  getRestaurantMenu(restaurantId: string, name: string) : Observable<any> {
    let url = `${this.serverUrl}/api/Restaurants/GetRestaurantMenuItems?restaurantId=${restaurantId}&name=${name}`;
    return this.http.get(url);
  }

  deleteMenuItem(restaurantId: string, menuItemId: string) {
    let url = `${this.serverUrl}/api/Restaurants/DeleteMenuItem?restaurantId=${restaurantId}&menuItemId=${menuItemId}`;
    return this.http.delete(url);
  }

}
