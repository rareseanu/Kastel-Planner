import { Component, OnInit } from '@angular/core';
import { Ticket } from '../shared/ticket.model';
import { TicketService } from '../shared/ticket.service';
import { ToastService } from '../toast/toast.service';
import {RouterModule} from '@angular/router';
@Component({
    templateUrl: 'tickets.component.html'
})
export class TicketsComponent implements OnInit {
    tickets: Ticket[];

    constructor(private ticketService: TicketService, public toastService: ToastService) {
    }

    danger() {
        this.toastService.danger("Danger notification test.");
    }

    success() {
        this.toastService.success("Success notification test.");

    }

    info() {
        this.toastService.info("Info notification test.");

    }

    ngOnInit(): void {
        this.ticketService.getAllTickets().subscribe(data => {
            this.tickets = data;
            console.log(data);
        })
    }
}