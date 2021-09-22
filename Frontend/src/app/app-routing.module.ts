import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './calendar';
import { ForgottenPasswordComponent } from './forgotten-password/forgotten-password.component';
import { AuthGuard } from './helpers/auth.guard';
import { LoginComponent } from './login';
import { PasswordResetComponent } from './password-reset/password-reset.component';
import { RegisterFormComponent } from './register-form/register-form.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard], data: {roles: ['Admin', 'Volunteer']}},
  { path: 'forgot-password', component: ForgottenPasswordComponent},
  { path: 'reset-password', component: PasswordResetComponent},
  { path: 'register', component: RegisterFormComponent},
  { path: '*', redirectTo: '/'}
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
