import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TicketService } from '../shared/ticket.service';
import { TicketMessage } from '../shared/ticketMessage.model';
import { AuthenticationService } from 'src/app/shared/authentication.service';
import { TicketMessageService } from '../shared/ticketMessage.service';
import { ToastService } from '../toast/toast.service';
import { Ticket } from '../shared/ticket.model';
@Component({
    templateUrl: 'ticket.component.html'
})
export class TicketComponent implements OnInit {
    ticketMessages: TicketMessage[];
    ticketMessageForm: FormGroup;
    newMessage: string;
    loading = false;
    submitted = false;
    error: string;
    ticket: Ticket;
    ticketId: string | null;

    constructor(private route: ActivatedRoute,
                 private ticketMessageService: TicketMessageService,
                 private ticketService: TicketService,
                 public toastService: ToastService,
                 private authenticationService: AuthenticationService) {
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

    get f() { return this.ticketMessageForm.controls; }


    sendMessage() {
        this.submitted = true;
        const ticketId = this.route.snapshot.paramMap.get('id');

        if (this.ticketMessageForm.invalid) {
            return;
        }

        let user = this.getCurrentUser();
        if(user && ticketId){
            this.ticketMessageService.createTicketMessage(this.f['message'].value, new Date(), ticketId, user.id).subscribe(data => {
                this.ticketMessages.push(data);
            })
        }
    }

    public getCurrentUser() {
        return this.authenticationService.getCurrentUser;
    }

    ngOnInit(): void {
        this.ticketId = this.route.snapshot.paramMap.get('id');

        if(this.ticketId) {
            this.ticketService.getTicketById(this.ticketId).subscribe(data => {
                this.ticket = data;
            });

            this.ticketMessageService.getTicketsByTicketId(this.ticketId).subscribe(data => {
                this.ticketMessages = data;
                this.ticketMessageForm = new FormGroup({
                    message: new FormControl('', [Validators.required, Validators.minLength(2)])
                })
                console.log(data);
            })
        }
    }
}