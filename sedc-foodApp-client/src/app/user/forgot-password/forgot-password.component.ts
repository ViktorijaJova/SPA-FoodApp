import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['../user.component.css', './forgot-password.component.css']
})
export class ForgotPasswordComponent {

  isLoading: boolean = false

  message: any = ""

  constructor(private userService: UserService) { }

  formModel = new FormGroup({
    EmailAddress: new FormControl('', Validators.required)
  })

  onSubmit() {

    let model = {
      Email: this.formModel.value.EmailAddress
    }

    this.isLoading = true;

    this.userService.forgotUserPassword(model).subscribe({
      next: res => {
        this.message = res.message
      },
      error: err => {
        this.message = err.error
      },
      complete: () => {
        this.isLoading = false
      }
    })

  } 
}
