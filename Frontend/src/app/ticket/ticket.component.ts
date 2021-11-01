import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../shared/ticket.model';
import { TicketService } from '../shared/ticket.service';
import { TicketMessage } from '../shared/ticketMessage.model';
import { AuthenticationService } from 'src/app/shared/authentication.service';
import { TicketMessageService } from '../shared/ticketMessage.service';
import { ToastService } from '../toast/toast.service';
import { User } from '../shared/user.model';
@Component({
    templateUrl: 'ticket.component.html'
})
export class TicketComponent implements OnInit {
    ticketMessages: TicketMessage[];
    ticketMessageForm: FormGroup;

    constructor(private route: ActivatedRoute,
                private ticketService: TicketService,
                 private ticketMessageService: TicketMessageService,
                 public toastService: ToastService,
                 private formBuilder: FormBuilder,
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

    sendMessage() {
        const ticketId = this.route.snapshot.paramMap.get('id');

        if (this.ticketMessageForm.invalid) {
            return;
        }

        let user = this.getCurrentUser();
        if(user && ticketId){
            this.ticketMessageService.createTicketMessage(this.ticketMessageForm.get('message')?.value, new Date(), user.id, ticketId).subscribe(data => {
                this.ticketMessages.push(data);
            })
        }
    }

    public getCurrentUser() {
        return this.authenticationService.getCurrentUser;
    }

    ngOnInit(): void {
        const ticketId = this.route.snapshot.paramMap.get('id');

        if(ticketId) {
            this.ticketMessageService.getTicketsByTicketId(ticketId).subscribe(data => {
                this.ticketMessages = data;
                this.ticketMessageForm = this.formBuilder.group({
                    message: ["", Validators.required]
                })
                console.log(data);
            })
        }
    }
}