import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { Person } from "./person.model";

@Injectable({ providedIn: 'root' })
export class PersonService {
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

    getAllPersons(): Observable<Person[]> {
        return this.http.get<Person[]>(`${environment.BASE_API_URL}/person`)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    updatePerson(person: Person) {
        console.log(person);
        return this.http.patch<Person>(`${environment.BASE_API_URL}/person/${person.id}`, person)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getById(id: string): Observable<Person> {
        return this.http.get<Person>(`${environment.BASE_API_URL}/person/${id}`)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }
}