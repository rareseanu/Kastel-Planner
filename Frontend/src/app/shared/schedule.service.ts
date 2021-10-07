import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { Schedule } from "./schedule.model";

@Injectable({ providedIn: 'root' })
export class ScheduleService {
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

    getAllSchedules(startTime?: Date, endTime?: Date): Observable<Schedule[]> {
        let params = new HttpParams();
        if(startTime) {
            params = params.append('startTime', startTime.toISOString());
        }
        
        if(endTime) {
            params = params.append('endTime', endTime.toISOString());
        }
        return this.http.get<Schedule[]>(`${environment.BASE_API_URL}/schedules`, {params: params})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getScheduleById(scheduleId: string):Observable<Schedule> {
        return this.http.get<Schedule>(`${environment.BASE_API_URL}/schedules/${scheduleId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    getSchedulesByPersonId(personId: string):Observable<Schedule[]> {
        return this.http.get<Schedule[]>(`${environment.BASE_API_URL}/schedules-by-id/${personId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    createSchedule(date: Date, duration: number, volunteerId: string, weeklyLogId: string) {
        return this.http.post<Schedule>(`${environment.BASE_API_URL}/schedule`, {date, minutes: duration, volunteerId, weeklyLogId})
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }

    updateSchedule(schedule: Schedule) {
        return this.http.patch<Schedule>(`${environment.BASE_API_URL}/schedules/schedule/${schedule.id}`, schedule)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getScheduleByPersonIdAndInterval(personId: string, startDate?: Date, endDate?: Date): Observable<Schedule[]> {
        let params = new HttpParams();
        if(startDate) {
            console.log(startDate);

            params = params.append('startTime', startDate.toISOString());
        }
        
        if(endDate) {
            params = params.append('endTime', endDate.toISOString());
        }
        return this.http.get<Schedule[]>(`${environment.BASE_API_URL}/schedules-by-id/${personId}`, {params: params})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }
}