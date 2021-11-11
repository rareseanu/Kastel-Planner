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
import { ToastService } from '../toast/toast.service';

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
    weeklyLogForm: FormGroup;
    loading = false;
    submitted = false;
    submittedLog = false;
    returnUrl: string;
    error: string;

    days: any = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday',
        'Sunday'
    ]

    static daysRecord: Record<string, number> = {
        "Monday": 1,
        "Tuesday": 2,
        "Wednesday": 3,
        "Thursday": 4,
        "Friday": 5,
        "Saturday": 6,
        "Sunday": 7
    };

    constructor(private personService: PersonService, private route: ActivatedRoute, private formBuilder: FormBuilder,
        private roleService: RoleService, private labelService: LabelService, private scheduleService: ScheduleService,
        private weeklyLogService: WeeklyLogService, public toastService: ToastService) {
    }

    ngOnInit(): void {
        this.loadPerson();
        console.log(this.getDateForThisWeek('Monday'));
    }

    getDateForThisWeek(dayOfWeek: string) {
        var curr = new Date;
        var monday = curr.getDate() - curr.getDay();
        return new Date(curr.setDate(monday + PersonDetailsComponent.daysRecord[dayOfWeek]));
    }

    onSubmitNewWeeklyLog() {
        this.submittedLog = true;

        if (this.weeklyLogForm.invalid) {
            return;
        }

        this.loading = true;
        this.weeklyLogService.createWeeklyLog(this.wf.startTime.value, this.wf.dayOfWeek.value, this.wf.minutes.value, this.person.id)
            .subscribe(response => {
                this.loading = false;
                this.weeklyLogService.getWeeklyLogById(response.id)
                    .subscribe(weeklyLog => {
                        this.weeklyLogs.push(weeklyLog);
                        this.toastService.success("Weekly log created successfully.");
                    })
            },
                (error) => {
                    this.error = error;
                    this.loading = false;
                });
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
    get wf() { return this.weeklyLogForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.person.roles = this.person.roles.filter((role, index, self) =>
            index === self.findIndex((r) => (
                r.roleName === role.roleName
            ))
        )
        this.personService.updatePerson(this.person).subscribe(response => {
            this.roleService.removeRolesFromPerson(this.person.id).subscribe(data => {
                for (let role of this.person.roles) {
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
            });
            this.labelService.removeLabelsFromPerson(this.person.id).subscribe(data => {
                for (let label of this.person.labels) {
                    this.labelService.addLabelToPerson(label.id, this.person.id)
                        .subscribe(
                            (data) => {
                                this.loading = false;
                            },
                            (error) => {
                                this.error = error;
                                this.loading = false;
                            });
                }
            });
            this.toastService.success(`${this.person.firstName} ${this.person.lastName} updated successfully.`);
        },
            (error) => {
                this.toastService.danger(error);
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

    changeDay(e: any) {
        this.weeklyLogForm.controls.dayOfWeek.setValue(e.target.value);
    }

    loadPerson() {
        const personId = this.route.snapshot.paramMap.get('id');

        if (personId) {
            this.personService.getById(personId).subscribe(person => {
                this.person = person;
                this.loginForm = this.formBuilder.group({
                    firstName: [this.person.firstName, Validators.required],
                    lastName: [this.person.lastName, Validators.required],
                    phoneNumber: [this.person.phoneNumber],
                    roles: [this.person.roles],
                    labels: [this.person.labels]
                });

                this.roleService.getAllRoles().subscribe(roles => {
                    this.roles = roles;
                    this.labelService.getAllLabels().subscribe(labels => {
                        this.labels = labels;

                        this.isBeneficiary = this.person.roles.some(r => r.roleName == 'Beneficiary');
                        if (this.isBeneficiary) {
                            this.weeklyLogService.getWeeklyLogsByPersonId(personId).subscribe(weeklyLogs => {
                                this.weeklyLogs = weeklyLogs;
                            });
                            this.weeklyLogForm = this.formBuilder.group({
                                startTime: ['', Validators.required],
                                dayOfWeek: ['', Validators.required],
                                minutes: [0, [Validators.required, Validators.min(1)]]
                            });
                        }

                        this.isVolunteer = this.person.roles.some(r => r.roleName === 'Volunteer');
                        if (this.isVolunteer) {
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