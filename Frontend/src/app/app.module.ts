import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login';
import { AccessTokenInterceptor } from './helpers/token.interceptor';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './shared/authentication.service';
import { CalendarComponent } from './calendar';
import { EventComponent } from './calendar/event/event.component';
import { PasswordResetComponent } from './password-reset/password-reset.component';
import { ForgottenPasswordComponent } from './forgotten-password/forgotten-password.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { RegisterFormComponent } from './register-form/register-form.component';
import { PersonFormComponent } from './person-form/person-form.component';
import { LabelFormComponent } from './label-form/label-form.component';
import { RoleFormComponent } from './role-form/role-form.component';
import { UserFormComponent } from './user-form/user-form.component';
import { BeneficiaryFormComponent } from './beneficiary-form/beneficiary-form.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CalendarComponent,
    EventComponent,
    PasswordResetComponent,
    ForgottenPasswordComponent,
    RegisterFormComponent,
    PersonFormComponent,
    LabelFormComponent,
    RoleFormComponent,
    UserFormComponent,
    BeneficiaryFormComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService]},
    { provide: HTTP_INTERCEPTORS, useClass: AccessTokenInterceptor, multi: true },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
