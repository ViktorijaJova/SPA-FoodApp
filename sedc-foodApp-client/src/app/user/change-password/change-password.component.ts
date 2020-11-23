import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['../user.component.css', './change-password.component.css']
})
export class ChangePasswordComponent {

  isLoading: boolean = false

  message: any = ""
  messageColor: any = "danger"

  constructor(private userService: UserService) { }

  formModel = new FormGroup({
    CurrentPassword: new FormControl('', Validators.required),
    NewPassword: new FormControl('', Validators.required),
  })

  onSubmit() {

    let model = {
      CurrentPassword: this.formModel.value.CurrentPassword,
      NewPassword: this.formModel.value.NewPassword
    }

    this.isLoading = true;

    this.userService.changeUserPassword(model).subscribe({
      next: res => {
        this.messageColor = "white"
        this.message = res.message
        this.formModel.reset()
      },
      error: err => {
        this.messageColor = "danger"
        this.message = err.error
        this.isLoading = false;
        this.formModel.reset()
      },
      complete: () => {
        this.isLoading = false;
      }
    }) 

  }

}
