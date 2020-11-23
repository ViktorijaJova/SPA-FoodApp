import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'
import { Token } from '../models/restaurant-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  serverUrl = environment.apiServer

  constructor(private http: HttpClient) { }

  register(body: any) : Observable<any> {
    let url = `${this.serverUrl}/api/applicationuser/register`;
    return this.http.post(url, body)
  }

  login(body: any) : Observable<any> {
    let url = `${this.serverUrl}/api/applicationuser/login`
    return this.http.post<Token>(url, body);
  }

  changeUserPassword(body: any) : Observable<any> {
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userId = payLoad.UserId
    body.UserId = userId

    let url = `${this.serverUrl}/api/applicationuser/ChangePassword`
    return this.http.post<any>(url, body);
  }

  forgotUserPassword(body: any) : Observable<any> {   
    let url = `${this.serverUrl}/api/applicationuser/ForgotPassword`
    return this.http.post<any>(url, body);
  }

  resetPassword(body: any) : Observable<any> {
    let url = `${this.serverUrl}/api/applicationuser/ResetPassword?email=${body.Email}&token=${body.Token}&newPassword=${body.NewPassword}`
    return this.http.get<any>(url)
  }
 
}
