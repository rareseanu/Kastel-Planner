import { Component, OnInit } from '@angular/core';
import { Ticket } from '../shared/ticket.model';
import { TicketService } from '../shared/ticket.service';
import { ToastService } from '../toast/toast.service';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthenticationService } from '../shared/authentication.service';

@Component({
    templateUrl: 'tickets.component.html'
})
export class TicketsComponent implements OnInit {
    tickets: Ticket[];
    newTicket: Ticket;
    newTicketForm: FormGroup;
    loading = false;
    submitted = false;
    error: string;

    types: any = [
        'Feature',
        'Bug'
    ]


    constructor(private ticketService: TicketService, public toastService: ToastService, public authService: AuthenticationService,
        private formBuilder: FormBuilder) {
    }

    get f() { return this.newTicketForm.controls; }

    changeType(e: any) {
        this.newTicketForm.controls.type.setValue(e.target.value);
    }

    onSubmit() {
        let user = this.authService.getCurrentUser;
        if (user) {
            this.submitted = true;

            if (this.newTicketForm.invalid) {
                return;
            }

            this.loading = true;
            this.ticketService.createTicket(this.f['subject'].value, "Open", new Date(), this.f['type'].value, user.id).subscribe(response => {
                this.loading = false;
                this.toastService.success("Ticket created successfully.");
                this.tickets.push(response);
            },
                (error) => {
                    this.error = error;
                    this.loading = false;
                });
        }
    }

    ngOnInit(): void {
        this.ticketService.getAllTickets().subscribe(data => {
            this.tickets = data;
            this.newTicketForm = new FormGroup({
                subject: new FormControl('', Validators.required),
                type: new FormControl('', Validators.required),
                description: new FormControl('', Validators.required)
            });
        });
    }
}