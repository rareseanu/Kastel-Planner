import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { WeeklyLog } from "./weekly-log.model";

@Injectable({ providedIn: 'root' })
export class WeeklyLogService {
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

    getWeeklyLogById(weeklyLogId: string) : Observable<WeeklyLog> {
        return this.http.get<WeeklyLog>(`${environment.BASE_API_URL}/weeklyLogs/${weeklyLogId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    getWeeklyLogsByPersonId(personId: string):Observable<WeeklyLog[]> {
        return this.http.get<WeeklyLog[]>(`${environment.BASE_API_URL}/weekly-logs-by-id/${personId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    updateWeeklyLog(weeklyLog: WeeklyLog): Observable<WeeklyLog> {
        console.log(weeklyLog.dayOfWeek.name);
        return this.http.patch<WeeklyLog>(`${environment.BASE_API_URL}/weeklylogs/weeklylog/${weeklyLog.id}`, 
                {startTime: weeklyLog.startTime, dayOfWeek: weeklyLog.dayOfWeek.name, id: weeklyLog.id, beneficiaryId: weeklyLog.beneficiaryId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    createWeeklyLog(startTime: string, dayOfWeek: string, beneficiaryId: string): Observable<WeeklyLog> {
        return this.http.post<WeeklyLog>(`${environment.BASE_API_URL}/weeklylog`, {startTime, dayOfWeek, beneficiaryId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }
}