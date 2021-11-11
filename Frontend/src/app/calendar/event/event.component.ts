import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/authentication.service';
import { Person } from 'src/app/shared/person.model';
import { Schedule } from 'src/app/shared/schedule.model';
import { ScheduleService } from 'src/app/shared/schedule.service';
import { WeeklyLog } from 'src/app/shared/weekly-log.model';
import { ToastService } from 'src/app/toast/toast.service';

@Component({
    selector: 'event',
    templateUrl: 'event.component.html',
    styleUrls: ['./event.component.css']
})
export class EventComponent {
    constructor(private scheduleService: ScheduleService, private authenticationService: AuthenticationService,
        private toastService: ToastService) {}

    public title = '';
    public leftEdge = 0;
    public topEdge = 0;
    public widthPercentage = 1;
    public height = 0;
    public zIndex = 0;
    public zIndexBackup = 0;

    public weeklyLog: WeeklyLog;
    public startHour: number;
    public endHour: number;
    public beneficiary: Person;
    public date: Date;

    mouseEnter() {
        this.zIndexBackup = this.zIndex;
        this.zIndex = 999;
    }

    mouseLeave() {
        this.zIndex = this.zIndexBackup;
    }

    assignableEvent(): boolean {
        if(this.date >= new Date()) {
            return false;
        }
        return true;
    }

    renderHour(hour: number) {
        let hours = Math.floor(hour);
        let minutes = Math.floor((hour - hours) * 60);
        if(hours > 24) {
            hours = hours % 24;
        }

        if(hours < 10) {
            if(minutes < 10) {
                return `0${hours}:0${minutes}`;
            } else {
                return `0${hours}:${minutes}`;
            }
        } else {
            if(minutes < 10) {
                return `${hours}:0${minutes}`;
            } else {
                return `${hours}:${minutes}`;
            }
        }
    }

    public hasRole(role: string): boolean {
        return this.authenticationService.hasRole(role);
    }

    public getCurrentUser() {
        return this.authenticationService.getCurrentUser;
    }

    public assignSchedule() {
        if(this.authenticationService.getCurrentUser) {
            this.scheduleService.createSchedule(this.date, this.authenticationService.getCurrentUser.personId, this.weeklyLog.id).subscribe(data => {
                this.weeklyLog.Schedule = data;
                this.toastService.info("Schedule assigned.");
            },
            (error) => {
                this.toastService.danger(error);
            });
        }
    }

    public unassignSchedule() {
        if(this.authenticationService.getCurrentUser && this.weeklyLog.Schedule) {
            this.scheduleService.deleteSchedule(this.weeklyLog.Schedule?.id).subscribe(data => {
                this.toastService.info("Schedule unassigned.");
            });
            this.weeklyLog.Schedule = null;
        }
    }
}