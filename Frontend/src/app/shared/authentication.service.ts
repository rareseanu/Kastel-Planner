import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError} from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { User } from './user.model';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User | null>;
    public currentUser: Observable<User | null>;

    constructor(private http: HttpClient, private router: Router) {
        this.currentUserSubject = new BehaviorSubject<User | null>(null);
        this.currentUser = this.currentUserSubject.asObservable();
        this.currentUser.subscribe();
    }

    public get getCurrentUser() {
        return this.currentUserSubject.getValue();
    }

    private handleError(err: HttpErrorResponse) {
        console.log(err);
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
        return this.http.post<User>(`${environment.BASE_API_URL}/login`, { email, password }, { withCredentials: true })
            .pipe(
                tap(data => { 
                    this.currentUserSubject.next(data);
                    this.startRefreshTokenTimer();
                    console.log("User logged in.");
                }),
                catchError(this.handleError)
            );
    }
    
    refreshToken(): Observable<User> {
        return this.http.post<User>(`${environment.BASE_API_URL}/user/refreshToken`, {}, { withCredentials: true })
            .pipe(
                tap(data => {
                    this.currentUserSubject.next(data);
                    this.startRefreshTokenTimer();
                    console.log("Token refreshed.");
                }),
                catchError(this.handleError)
            );
    }

    logout() {
        this.http.post<any>(`${environment.BASE_API_URL}/user/revokeToken`, {}, {withCredentials:true}).subscribe();
        this.stopRefreshTokenTimer();
        this.currentUserSubject.next(null);
    }

    private refreshTokenTimeout: any;

    private startRefreshTokenTimer() {
        if(this.getCurrentUser) {
            const jwtToken = JSON.parse(atob(this.getCurrentUser.token.split('.')[1]));

            const expires = new Date(jwtToken.exp * 1000);
            const timeout = expires.getTime() - Date.now() - (5 * 1000);
            this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
        }
    }

    private stopRefreshTokenTimer() {
        clearTimeout(this.refreshTokenTimeout);
    }
}