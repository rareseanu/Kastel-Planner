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
import { PersonRole } from './person-role.model';
import { Beneficiary } from './beneficiary.model';
import { InsertUser } from './insert-user.model';

@Injectable({ providedIn: 'root' })
export class RegisterService {

    private currentPersonSubject: BehaviorSubject<Person | null>;
    private personLabelSubject: BehaviorSubject<PersonLabel | null>;
    private personRoleSubject: BehaviorSubject<PersonRole | null>;
    private personWeeklyLogSubject: BehaviorSubject<Beneficiary | null>;
    private userEmailSubject: BehaviorSubject<InsertUser | null>;

    public currentPerson: Observable<Person | null>;
    public currentPersonLabel: Observable<PersonLabel | null>;
    public currentPersonRole: Observable<PersonRole | null>;
    public currentWeeklyLog: Observable<Beneficiary | null>;
    public currentUserEmail: Observable<InsertUser | null>;

    constructor(private http: HttpClient, private router: Router) {

        this.currentPersonSubject = new BehaviorSubject<Person | null>(null);
        this.currentPerson = this.currentPersonSubject.asObservable();
        this.currentPerson.subscribe();

        this.personLabelSubject= new BehaviorSubject<PersonLabel | null>(null);;
        this.currentPersonLabel = this.personLabelSubject.asObservable();
        this.currentPersonLabel.subscribe();

        this.personRoleSubject= new BehaviorSubject<PersonRole | null>(null);;
        this.currentPersonRole = this.personRoleSubject.asObservable();
        this.currentPersonRole.subscribe();

        this.personWeeklyLogSubject= new BehaviorSubject<Beneficiary | null>(null);;
        this.currentWeeklyLog = this.personWeeklyLogSubject.asObservable();
        this.currentWeeklyLog.subscribe();

        this.userEmailSubject= new BehaviorSubject<InsertUser | null>(null);;
        this.currentUserEmail = this.userEmailSubject.asObservable();
        this.currentUserEmail.subscribe();
    }

    public get getCurrentPerson() {
        return this.currentPersonSubject.getValue();
    }

    public get getCurrentPersonLabel() {
        return this.personLabelSubject.getValue();
    }

    public get getCurrentPersonRole() {
        return this.personRoleSubject.getValue();
    }

    
    public get getCurrentWeeklyLog() {
        return this.personWeeklyLogSubject.getValue();
    }

    public get getCurrentUserEmail() {
        return this.userEmailSubject.getValue();
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
                    console.log("Inserted person");
                }),
                catchError(this.handleError)
            );
    }

    insertPersonRole(roleId: string, personId: string): Observable<PersonRole>
    {
        
        console.log("Role id from register service" + " " +roleId);
        console.log("Person id from register service" + " " +personId);

        return this.http.post<PersonRole>(`${environment.BASE_API_URL}/person-role`, {roleId, personId})
            .pipe(
                tap(data => { 
                    this.personRoleSubject.next(data);
                    console.log("Inserted person role.");
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
            map(
                (response: any) => response)
            
            );
           
        
    }

    insertPersonLabel(labelId: string, personId: string): Observable<PersonLabel>
    {
        
        console.log("Label id from service" + " " +labelId);
        console.log("person id from service" + " " +personId);

        return this.http.post<PersonLabel>(`${environment.BASE_API_URL}/person-label`, {labelId, personId})
            .pipe(
                tap(data => { 
                    this.personLabelSubject.next(data);
                    console.log("Inserted person label.");
                }),
                catchError(this.handleError)
            );
    }

    insertWeeklyLog(startTime: string, dayOfWeek: string, BeneficiaryId: string): Observable<Beneficiary>
    {
    
        return this.http.post<Beneficiary>(`${environment.BASE_API_URL}/weeklylog`, {startTime, dayOfWeek,BeneficiaryId })
            .pipe(
                tap(data => { 
                    this.personWeeklyLogSubject.next(data);
                    console.log("Inserted weeklylog.");
                }),
                catchError(this.handleError)
            );
    }

    insertUserEmail(email: string, personId: string, password:string): Observable<InsertUser>
    {
    
        return this.http.post<InsertUser>(`${environment.BASE_API_URL}/user`, {email, personId, password})
            .pipe(
                tap(data => { 
                    this.userEmailSubject.next(data);
                    console.log("Inserted email.");
                }),
                catchError(this.handleError)
            );
    }


 
}