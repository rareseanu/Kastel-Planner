import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { Ticket } from "./ticket.model";
import { catchError, tap } from "rxjs/operators";
import { TicketMessage } from "./ticketMessage.model";

@Injectable({ providedIn: 'root' })
export class TicketMessageService {
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

    createTicketMessage(message: string, sentAt: Date, ticketId: string, userId: string): Observable<TicketMessage> {
        return this.http.post<TicketMessage>(`${environment.BASE_API_URL}/ticketmessage`, {message, sentAt, ticketId, userId})
            .pipe(
                tap(data => {
                    console.log(data);
                }),
                catchError(this.handleError)
            );
    }

    getTicketsByTicketId(ticketId: string) : Observable<TicketMessage[]> {
        return this.http.get<TicketMessage[]>(`${environment.BASE_API_URL}/ticketMessage/ticketmessages-by-id/${ticketId}`)
        .pipe(
            tap(data => {
                console.log(data);
            }),
            catchError(this.handleError)
        );
    }
}