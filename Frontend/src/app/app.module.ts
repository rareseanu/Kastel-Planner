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
import { RegisterFormComponent } from './register-form/register-form.component';
import { PersonFormComponent } from './person-form/person-form.component';
import { LabelFormComponent } from './label-form/label-form.component';
import { RoleFormComponent } from './role-form/role-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterFormComponent,
    PersonFormComponent,
    LabelFormComponent,
    RoleFormComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService]},
    { provide: HTTP_INTERCEPTORS, useClass: AccessTokenInterceptor, multi: true },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
