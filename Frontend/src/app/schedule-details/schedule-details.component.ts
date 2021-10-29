import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { Schedule } from "../shared/schedule.model";
import { ScheduleService } from "../shared/schedule.service";

@Component({
    templateUrl: 'schedule-details.component.html'
})
export class ScheduleDetailsComponent implements OnInit {
    schedule: Schedule;
    scheduleEditForm: FormGroup;    
    loading = false;
    submitted = false;
    error: any;

    constructor(private route: ActivatedRoute, private scheduleService: ScheduleService,  private formBuilder: FormBuilder) {}

    ngOnInit() {
        this.loadSchedule();
    }

    get f() { return this.scheduleEditForm.controls; }
    
    loadSchedule() {
        const scheduleId = this.route.snapshot.paramMap.get('id');

        if(scheduleId) {
            this.scheduleService.getScheduleById(scheduleId).subscribe(data => {
                this.schedule = data;
                this.scheduleEditForm = this.formBuilder.group({
                    duration: [this.schedule.weeklyLog.duration, Validators.required],
                    date: [this.schedule.date, Validators.required]
                });
            });
        }
    }

    onSubmit() {
        this.submitted = true;

        if (this.scheduleEditForm.invalid) {
            return;
        }

        this.loading = true;
        this.scheduleService.updateSchedule(this.schedule).subscribe(response => {
            this.loading = false;
        },
            (error) => {
                this.error = error;
                this.loading = false;
        });
    }

}