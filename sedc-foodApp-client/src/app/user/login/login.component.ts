import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../user.component.css', './login.component.css']
})
export class LoginComponent implements OnInit {

  isLoading: boolean = false
  message: string = ""

  constructor(private userService: UserService,
              private router: Router,
              private authService: AuthService,
              private orderService: OrderService) { }

  ngOnInit(): void {
    if (localStorage.getItem("token") != null) {
      this.router.navigateByUrl("/home")
    }
  }

  formModel = new FormGroup({
    UserName: new FormControl('', Validators.required),
    Password: new FormControl('', Validators.required),
  })

  onSubmit() {
    let body = {
      Username: this.formModel.value.UserName,
      Password: this.formModel.value.Password
    }

    this.isLoading = true

    this.userService.login(body).subscribe({
      next: res => {
        localStorage.setItem("token", res.token)
        this.authService.checkIfUserIsLogged()
        this.authService.checkIfUserIsAdmin()
      },
      error: err => {
        this.message = err.error
        this.formModel.reset()
        this.isLoading = false
      },
      complete: () => {
        this.createEmptyOrder()

        this.isLoading = false
        this.router.navigateByUrl("/restaurants")
      }
    })
  }

  createEmptyOrder() {
    let userId = this.authService.getUserId()
    let request = {
      UserId: userId
    }

    this.orderService.createOrder(request).subscribe({
      error: err => console.warn(err.error)
    })
  }

}
