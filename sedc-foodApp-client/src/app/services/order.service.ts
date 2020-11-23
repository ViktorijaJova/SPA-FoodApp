import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  serverUrl = environment.apiServer

  constructor(private http: HttpClient) { }

  createOrder(request: any) : Observable<any> {
    let url = `${this.serverUrl}/api/Order/CreateOrder`;
    return this.http.post(url, request);
  }

  updateOrder(request: any) : Observable<any> {
    let url = `${this.serverUrl}/api/Order/UpdateOrder`;
    return this.http.post(url, request);
  }

  getUserOrder(userId: string) {
    let url = `${this.serverUrl}/api/Order/GetOrder?UserId=${userId}`;
    return this.http.get(url);
  }

}
