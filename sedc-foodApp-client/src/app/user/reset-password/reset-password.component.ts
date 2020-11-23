import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['../user.component.css', './reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  userEmail: string
  token: string

  message: any
  isLoading: boolean

  constructor(private route: ActivatedRoute,
              private fb: FormBuilder,
              private userService: UserService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: params => {
        this.userEmail = params.email
        this.token = params.token
      }
    })
  }

  formModel = this.fb.group({
    Password: ['', [Validators.required, Validators.minLength(4)]],
    ConfirmPassword: ['', Validators.required]
  }, {validator: this.comparePasswords})

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  onSubmit() {

    let model = {
      NewPassword : this.formModel.value.Password,
      Email : this.userEmail,
      Token : this.token
    }

    this.isLoading = true

    this.userService.resetPassword(model).subscribe({
      next: res => {
        this.message = res.message
      },
      error: err => {
        this.message = err.error
        this.isLoading = false
      },
      complete: () => {
        this.isLoading = false
      }
    })

  } 

}
