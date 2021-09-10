import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError} from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { User } from './user.model';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    public currentUser: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);

    constructor(private http: HttpClient) {
    }

    public getUser() {
        return this.currentUser.asObservable();
    }

    private handleError(err: HttpErrorResponse) {
        let errorMessage = '';

        if(err.error instanceof ErrorEvent) {
            errorMessage = `Client-side/network error: ${err.error.message}`;
        } else {
            console.error(`Server returned code: ${err.status}, error message: ${err.error}`);
            errorMessage = err.error;
        }
        return throwError(errorMessage);
    }

    login(email: string, password: string): Observable<User> {
        return this.http.post<User>(`${environment.BASE_API_URL}/user/login`, { email, password })
            .pipe(
                tap(data => { 
                    this.currentUser.next(data);
                    this.setCookie("jwtToken", data.token, 15, '/');
                }),
                catchError(this.handleError)
            );
    }

    private setCookie(name: string, value: string, minutes: number, path: string = '') {
        let d:Date = new Date();
        d.setTime(d.getTime() + minutes * 60 * 60 * 1000);
        let expires:string = `expires=${d.toUTCString()}`;
        let cpath:string = path ? `; path=${path}` : '';
        document.cookie = `${name}=${value}; secure;${expires}${cpath}`;
    }

    public getCookie(name: string) {
        let ca: Array<string> = document.cookie.split(';');
        let caLen: number = ca.length;
        let cookieName = `${name}=`;
        let c: string;

        for (let i: number = 0; i < caLen; i += 1) {
            c = ca[i].replace(/^\s+/g, '');
            if (c.indexOf(cookieName) == 0) {
                return c.substring(cookieName.length, c.length);
            }
        }
        return '';
    }

    logout() {
        
        this.currentUser.next(null);
    }
}