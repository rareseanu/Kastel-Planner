import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './calendar';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ForgottenPasswordComponent } from './forgotten-password/forgotten-password.component';
import { AuthGuard } from './helpers/auth.guard';
import { LoginComponent } from './login';
import { PasswordResetComponent } from './password-reset/password-reset.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import { ScheduleDetailsComponent } from './schedule-details/schedule-details.component';
import { WeeklyLogComponent } from './weekly-log/weekly-log.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  { path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard], data: {roles: ['Admin', 'Volunteer']}},
  { path: 'forgot-password', component: ForgottenPasswordComponent},
  { path: 'reset-password', component: PasswordResetComponent},
  { path: 'register', component: RegisterFormComponent, canActivate: [AuthGuard], data: {roles: ['Admin']}},
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard], data: {roles: ['Admin']}},
  { path: 'person/:id', component: PersonDetailsComponent, canActivate: [AuthGuard], data: {roles: ['Admin']}},
  { path: 'schedule/:id', component: ScheduleDetailsComponent, canActivate: [AuthGuard], data: {roles: ['Admin']}},
  { path: 'weekly-log/:id', component: WeeklyLogComponent, canActivate: [AuthGuard], data: {roles: ['Admin']}},
  { path: '**', redirectTo: 'home'}
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
