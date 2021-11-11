import { Component, OnInit } from '@angular/core';
import { Person } from '../shared/person.model';
import { PersonService } from '../shared/person.service';
import { ToastService } from '../toast/toast.service';
@Component({
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    persons: Person[];

    constructor(private personService: PersonService, public toastService: ToastService) {
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
        this.personService.getAllPersons().subscribe(data => {
            this.persons = data;
            console.log(data);
        })
    }

    anonymize(person: Person) {
        if(confirm("Are you sure you want to anonymize this user?")) {
            this.personService.anonymize(person).subscribe(response => {
                person = response;
            });
        }
    } 

    toggleIsActive(person: Person) {
        if(!person.isActive || (person.isActive && confirm("Are you sure you want to disable this user?"))) {
            person.isActive = !person.isActive;
            this.personService.updatePerson(person).subscribe(response => {
                console.log(response);
            });
        }
    }
}