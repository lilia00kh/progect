import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResetMyPasswordComponent } from './reset-my-password/reset-my-password.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { AuthGuard } from './../shared/guards/auth.guard';

@NgModule({
  declarations: [RegisterUserComponent, LoginComponent, ForgotPasswordComponent, ResetPasswordComponent,ResetMyPasswordComponent, EmailConfirmationComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'register', component: RegisterUserComponent },
      { path: 'login', component: LoginComponent },
      { path: 'forgotpassword', component: ForgotPasswordComponent },
      { path: 'resetpassword', component: ResetPasswordComponent },
      { path: 'resetmypassword', component: ResetMyPasswordComponent , canActivate: [AuthGuard] },
      { path: 'emailconfirmation', component: EmailConfirmationComponent }
    ])
  ]
})
export class AuthenticationModule { }
