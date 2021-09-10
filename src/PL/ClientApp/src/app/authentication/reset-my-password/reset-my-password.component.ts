import { ResetPasswordDto } from './../../_interfaces/resetPassword/resetPasswordDto.model';
import { ActivatedRoute } from '@angular/router';
import { PasswordConfirmationValidatorService } from './../../shared/custom-validators/password-confirmation-validator.service';
import { AuthenticationService } from './../../shared/services/authentication.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-my-password.component.html',
  styleUrls: ['./reset-my-password.component.css']
})
export class ResetMyPasswordComponent implements OnInit {
  public isUserAuthenticated: boolean;
  public resetPasswordForm: FormGroup;
  public showSuccess: boolean;
  public showError: boolean;
  public errorMessage: string;

  private _token: string;
  private _email: string;

  constructor(private _authService: AuthenticationService, private _passConfValidator: PasswordConfirmationValidatorService,
              private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      });
      
    this.resetPasswordForm = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('')
    });
    this.resetPasswordForm.get('confirm').setValidators([Validators.required,
      this._passConfValidator.validateConfirmPassword(this.resetPasswordForm.get('password'))]);

      if(this._authService.isUserAuthenticated){
        this._token = localStorage.getItem('token');
      }
    this._token = this._route.snapshot.queryParams['token'];
    this._email = this._route.snapshot.queryParams['email'];
    
  }

  public validateControl = (controlName: string) => {
    return this.resetPasswordForm.controls[controlName].invalid && this.resetPasswordForm.controls[controlName].touched;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.resetPasswordForm.controls[controlName].hasError(errorName);
  }

  public resetPassword = (resetPasswordFormValue) => {
    if (!resetPasswordFormValue.password.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,100})/)) {
      this.showError = true;
      this.errorMessage = "Пароль повиннен містити щонайменше 8 символів, як мінімум одну цифру, одну велику і одну малу літери, та один із символів ~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \ | . , : ;.";
      return;
    }
    this.showError = this.showSuccess = false;

    const resetPass = { ... resetPasswordFormValue };
    const resetPassDto: ResetPasswordDto = {
      password: resetPass.password,
      confirmPassword: resetPass.confirm,
      token: this._token,
      email: this._email
    };

    this._authService.resetPassword('api/accounts/resetpassword', resetPassDto)
      .subscribe(_ => {
          this.showSuccess = true;
        },
        error => {
          this.showError = true;
          this.errorMessage = error;
        });
  }
}
