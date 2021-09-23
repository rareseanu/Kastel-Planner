import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Person } from '../shared/person.model';
import { PersonService } from '../shared/person.service';
import { Role } from '../shared/role';
import { RoleService } from '../shared/role.service';
import { LabelService } from '../shared/label.service';
import { Label } from '../shared/label';
import { ScheduleService } from '../shared/schedule.service';
import { Schedule } from '../shared/schedule.model';
import { WeeklyLog } from '../shared/weekly-log.model';
import { WeeklyLogService } from '../shared/weekly-log.service';

@Component({
    templateUrl: 'person-details.component.html',
    styleUrls: ['person-details.component.css']
})
export class PersonDetailsComponent implements OnInit {
    person: Person;
    isVolunteer: boolean;
    isBeneficiary: boolean;
    schedules: Schedule[];
    reportSchedules: Schedule[];
    weeklyLogs: WeeklyLog[];

    roles: Role[];
    labels: Label[];
    loginForm: FormGroup;
    reportForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error: string;

    constructor(private personService: PersonService, private route: ActivatedRoute, private formBuilder: FormBuilder,
            private roleService: RoleService, private labelService: LabelService, private scheduleService: ScheduleService,
            private weeklyLogService: WeeklyLogService) {
    }

    ngOnInit(): void {
        this.loadPerson();
    }

    onSubmitReportForm() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        console.log(this.rf.startDate.value);
        this.scheduleService.getScheduleByPersonIdAndInterval(this.person.id, new Date(this.rf.startDate.value), new Date(this.rf.endDate.value))
            .subscribe(response => {
                this.reportSchedules = response;
                this.loading = false;
            },
            (error) => {
                this.error = error;
                this.loading = false;
            });
    }

    get f() { return this.loginForm.controls; }
    get rf() { return this.reportForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;

        this.personService.updatePerson(this.person).subscribe(response => {
            for(let role of this.person.roles) {
                this.roleService.addRoleToPerson(role.id, this.person.id)
                .subscribe(
                    (data) => {
                        this.loading = false;
                    },
                    (error) => {
                        this.error = error;
                        this.loading = false;
                    });
            }
        },
            (error) => {
                this.error = error;
                this.loading = false;
        });
    }

    deleteSchedule(schedule: Schedule) {
        schedule.volunteerId = null;
        this.scheduleService.updateSchedule(schedule).subscribe(data => {
            const index = this.schedules.indexOf(schedule, 0);
            if (index > -1) {
                this.schedules.splice(index, 1);
            }
        });
    }

    loadPerson() {
        const personId = this.route.snapshot.paramMap.get('id');

        if(personId) {
            this.personService.getById(personId).subscribe(person => {
                this.person = person;
                this.loginForm = this.formBuilder.group({
                    firstName: [this.person.firstName, Validators.required],
                    lastName: [this.person.lastName, Validators.required],
                    phoneNumber: [this.person.phoneNumber, Validators.required],
                    roles: [this.person.roles],
                    labels: [this.person.labels]
                });

                this.roleService.getAllRoles().subscribe(roles => {
                    this.roles = roles;
                    this.labelService.getAllLabels().subscribe(labels => {
                        this.labels = labels;
                        
                        this.isBeneficiary = this.person.roles.some(r => r.roleName == 'Beneficiary');
                        if(this.isBeneficiary) {
                            this.weeklyLogService.getWeeklyLogsByPersonId(personId).subscribe(weeklyLogs => {
                                this.weeklyLogs = weeklyLogs;
                            })
                        }

                        this.isVolunteer = this.person.roles.some(r => r.roleName === 'Volunteer');
                        if(this.isVolunteer) {
                            this.scheduleService.getSchedulesByPersonId(personId).subscribe(schedules => {
                                this.schedules = schedules;
                            })
                            this.reportForm = this.formBuilder.group({
                                startDate: [new Date(), Validators.required],
                                endDate: [new Date(), Validators.required]
                            })
                        }
                    });
                });
            });
        }
    }
}