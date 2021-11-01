import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { Ticket } from "./ticket.model";
import { catchError, tap } from "rxjs/operators";
import { Role } from "./role";

@Injectable({ providedIn: 'root' })
export class TicketService {
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

    getAllTickets(): Observable<Ticket[]> {
        return this.http.get<Ticket[]>(`${environment.BASE_API_URL}/ticket`)
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    createTicket(subject: string, status: string, openedDate: Date, type: string, userId: string): Observable<Ticket> {
        return this.http.post<Ticket>(`${environment.BASE_API_URL}/ticket`, {subject, status, openedDate, type, userId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getTicketsByUserId(userId: string) : Observable<Ticket[]> {
        return this.http.get<Ticket[]>(`${environment.BASE_API_URL}/ticket-by-id/${userId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }
}