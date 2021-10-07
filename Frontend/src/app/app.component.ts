import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/authentication.service';
import { ToastService } from './toast/toast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  currentUser: any;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private toastService: ToastService
  ) {
  }

  ngOnInit() {
    this.authenticationService.currentUser.subscribe(
      (data) => this.currentUser = data);
  }

  logout() {
    console.log("LOGOUT");
    this.authenticationService.logout();
    this.toastService.success("Logged out successfully.");
  }

}
