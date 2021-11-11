import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { Role } from "./role";
@Injectable({ providedIn: 'root' })
export class RoleService {
    constructor(private http: HttpClient) {

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

    addRoleToPerson(roleId: string, personId: string) {
        return this.http.post(`${environment.BASE_API_URL}/personRole`, {roleId, personId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    removeRolesFromPerson(personId: string) {
        return this.http.post(`${environment.BASE_API_URL}/personRole/removeRoles`, {personId: personId})
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    getAllRoles(): Observable<Role[]> {
        return this.http.get<Role[]>(`${environment.BASE_API_URL}/role`)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

}