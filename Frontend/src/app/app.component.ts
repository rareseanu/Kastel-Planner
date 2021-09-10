import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './shared/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  currentUser: any;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.currentUser = this.authenticationService.getUser().subscribe;
    console.log(this.currentUser.email);
  }

  ngOnInit() {
    this.authenticationService.getUser().subscribe(
      (data) => this.currentUser = data);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
