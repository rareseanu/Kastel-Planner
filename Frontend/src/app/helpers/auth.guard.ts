import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from '../shared/authentication.service';

@Injectable({ providedIn: 'root'})
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.authenticationService.getCurrentUser;

        if (user) {
            if (route.data.roles) {
                for (let role in route.data.roles) {
                    if(user.roles.indexOf(route.data.roles[role]) > -1) {
                        return true;
                    }
                }
                this.router.navigate(['/']);
                return false;
            }
        }

        this.router.navigate(['login']);
        return false;
    }
}