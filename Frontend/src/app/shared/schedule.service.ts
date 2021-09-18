import { HttpClient, HttpErrorResponse } from "@angular/common/http";
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

    getAllSchedules(): Observable<Schedule[]> {
        return this.http.get<Schedule[]>(`${environment.BASE_API_URL}/schedules`)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }
}