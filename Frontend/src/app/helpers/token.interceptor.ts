import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { merge, Observable } from 'rxjs';

import { AuthenticationService } from '../shared/authentication.service';
import { environment } from 'src/environments/environment';
import { mergeMap, tap } from 'rxjs/operators';

@Injectable()
export class AccessTokenInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const user = this.authenticationService.getCurrentUser;

        if(user != null) {
            const isApiUrl = request.url.startsWith(environment.BASE_API_URL);

            if(isApiUrl) {
                request = request.clone({
                    setHeaders: { Authorization: `Bearer ${user.token}` }
                });
            }
        }

        return next.handle(request);
    }
}