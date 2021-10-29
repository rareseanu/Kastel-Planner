import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/authentication.service';
@Component({
    templateUrl: 'home.component.html'
})
export class HomeComponent {
    constructor(private authService: AuthenticationService) {}

    public getCurrentUser() {
        return this.authService.getCurrentUser;
    }
}