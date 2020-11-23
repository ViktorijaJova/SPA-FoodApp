import { ThrowStmt } from '@angular/compiler';
import { Component, DoCheck, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements DoCheck {

  isLoggedIn: boolean = false
  isUserAdmin: boolean = false

  constructor(public authService: AuthService) { }

  ngDoCheck(): void {
    if(!!localStorage.getItem("token")) {
      this.authService.isLoggedIn.subscribe({
        next: data => this.isLoggedIn = data
      })

      this.authService.isAdmin.subscribe({
        next: data => this.isUserAdmin = data
      }) 

      this.authService.checkIfUserIsLogged()
      this.authService.checkIfUserIsAdmin()
    }
  }
  
  onLogout() {
    this.authService.logout()
  }

}
