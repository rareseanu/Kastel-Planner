import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './calendar';
import { AuthGuard } from './helpers/auth.guard';
import { LoginComponent } from './login';
import { Role } from './shared/role';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard], data: {roles: ['Admin', 'Volunteer']}},
  { path: '*', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
