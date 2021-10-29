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

    getAllWeeklyLogs(startTime?: Date, endTime?: Date): Observable<WeeklyLog[]> {
        let params = new HttpParams();
        if(startTime) {
            params = params.append('startTime', startTime.toISOString());
        }
        
        if(endTime) {
            params = params.append('endTime', endTime.toISOString());
        }
        return this.http.get<WeeklyLog[]>(`${environment.BASE_API_URL}/beneficiaryWeeklyLog`, {params: params})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getWeeklyLogById(weeklyLogId: string) : Observable<WeeklyLog> {
        return this.http.get<WeeklyLog>(`${environment.BASE_API_URL}/beneficiaryWeeklyLog/${weeklyLogId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    getWeeklyLogsByPersonId(personId: string):Observable<WeeklyLog[]> {
        return this.http.get<WeeklyLog[]>(`${environment.BASE_API_URL}/beneficiaryWeeklyLog/weekly-logs-by-id/${personId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    updateWeeklyLog(weeklyLog: WeeklyLog): Observable<WeeklyLog> {
        console.log(weeklyLog.dayOfWeek.name);
        return this.http.patch<WeeklyLog>(`${environment.BASE_API_URL}/beneficiaryWeeklyLog/${weeklyLog.id}`, 
                {startTime: weeklyLog.startTime, dayOfWeek: weeklyLog.dayOfWeek.name, beneficiaryId: weeklyLog.beneficiaryId, minutes: weeklyLog.duration})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    createWeeklyLog(startTime: string, dayOfWeek: string, minutes: number, beneficiaryId: string): Observable<WeeklyLog> {
        return this.http.post<WeeklyLog>(`${environment.BASE_API_URL}/beneficiaryWeeklyLog`, {startTime, dayOfWeek, minutes, beneficiaryId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }
}