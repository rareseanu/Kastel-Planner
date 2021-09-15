import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError} from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Person } from './person.model';
import { Label } from './label.model';
import { PersonLabel } from './person-label.model';
import { Role } from './role.model';

@Injectable({ providedIn: 'root' })
export class RegisterService {
    private currentPersonSubject: BehaviorSubject<Person | null>;
    private personLabelSubject: BehaviorSubject<PersonLabel | null>;
    public currentPerson: Observable<Person | null>;
    public currentPersonLabel: Observable<PersonLabel | null>;

    constructor(private http: HttpClient, private router: Router) {

        this.currentPersonSubject = new BehaviorSubject<Person | null>(null);
        this.currentPerson = this.currentPersonSubject.asObservable();
        this.currentPerson.subscribe();

        this.personLabelSubject= new BehaviorSubject<PersonLabel | null>(null);;
        this.currentPersonLabel = this.personLabelSubject.asObservable();
        this.currentPersonLabel.subscribe();
    }

    public get getCurrentPerson() {
        return this.currentPersonSubject.getValue();
    }

    public get getCurrentPersonLabel() {
        return this.personLabelSubject.getValue();
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

    register(firstName: string, lastName: string, phoneNumber: string, isActive: boolean): Observable<Person> {
        return this.http.post<Person>(`${environment.BASE_API_URL}/person`, {firstName, lastName, phoneNumber, isActive}, { withCredentials: true })
            .pipe(
                tap(data => { 
                    this.currentPersonSubject.next(data);
                    console.log("Inserted person.");
                }),
                catchError(this.handleError)
            );
    }

    getLabelsFromAPI():Observable<Label[]>{
        return this.http.get<Label[]>(`${environment.BASE_API_URL}/labels`)
        .pipe(
            map((response: any) => response));
        
    }

    getRolesFromAPI():Observable<Role[]>{
        return this.http.get<Role[]>(`${environment.BASE_API_URL}/roles`)
        .pipe(
            map((response: any) => response));
        
    }

    insertPersonLabel(labelId: string, personId: string): Observable<PersonLabel>
    {
        
        console.log("Label id" + " " +labelId);

        return this.http.post<PersonLabel>(`${environment.BASE_API_URL}/person-label`, {labelId, personId})
            .pipe(
                tap(data => { 
                    this.personLabelSubject.next(data);
                    console.log("Inserted person label.");
                }),
                catchError(this.handleError)
            );
    }
    
}