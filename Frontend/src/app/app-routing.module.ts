import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalendarComponent } from './calendar';
import { LoginComponent } from './login';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'calendar', component: CalendarComponent},
  { path: '*', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
