import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../shared/person.model';
import { PersonService } from '../shared/person.service';
@Component({
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    persons: Person[];

    constructor(private personService: PersonService) {
    }

    ngOnInit(): void {
        this.personService.getAllPersons().subscribe(data => {
            this.persons = data;
            console.log(data);
        })
    }
}