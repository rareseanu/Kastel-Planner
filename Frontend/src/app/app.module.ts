import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login';
import { AccessTokenInterceptor } from './helpers/token.interceptor';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './shared/authentication.service';
import { CalendarComponent } from './calendar';
import { EventComponent } from './calendar/event/event.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CalendarComponent,
    EventComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService]},
    { provide: HTTP_INTERCEPTORS, useClass: AccessTokenInterceptor, multi: true },
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
