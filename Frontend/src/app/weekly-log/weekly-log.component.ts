import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { WeeklyLog } from "../shared/weekly-log.model";
import { WeeklyLogService } from "../shared/weekly-log.service";

@Component({
    templateUrl: 'weekly-log.component.html'
})
export class WeeklyLogComponent implements OnInit {
    weeklyLog: WeeklyLog;
    weeklyLogEditForm: FormGroup;    
    loading = false;
    submitted = false;
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

    constructor(private route: ActivatedRoute, private weeklyLogService: WeeklyLogService, private formBuilder: FormBuilder) {}

    ngOnInit() {
        this.loadWeeklyLog();
    }

    get f() { return this.weeklyLogEditForm.controls; }
    
    loadWeeklyLog() {
        const weeklyLog = this.route.snapshot.paramMap.get('id');

        if(weeklyLog) {
            this.weeklyLogService.getWeeklyLogById(weeklyLog).subscribe(data => {
                this.weeklyLog = data;
                this.weeklyLogEditForm = this.formBuilder.group({
                    startTime: [this.weeklyLog.startTime, Validators.required],
                    dayOfWeek: [this.weeklyLog.dayOfWeek, Validators.required],
                    duration: [this.weeklyLog.duration, Validators.required]
                });
            });
        }
    }

    changeDay(e: any) {
        this.weeklyLog.dayOfWeek.name = e.target.value;
    }

    onSubmit() {
        this.submitted = true;

        if (this.weeklyLogEditForm.invalid) {
            return;
        }

        this.loading = true;
        this.weeklyLogService.updateWeeklyLog(this.weeklyLog).subscribe(response => {
            this.loading = false;
        },
            (error) => {
                this.error = error;
                this.loading = false;
        });
    }

}