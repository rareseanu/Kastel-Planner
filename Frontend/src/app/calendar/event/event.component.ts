import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/authentication.service';
import { Schedule } from 'src/app/shared/schedule.model';
import { ScheduleService } from 'src/app/shared/schedule.service';

@Component({
    selector: 'event',
    templateUrl: 'event.component.html',
    styleUrls: ['./event.component.css']
})
export class EventComponent {
    constructor(private scheduleService: ScheduleService, private authenticationService: AuthenticationService) {}

    public title = '';
    public leftEdge = 0;
    public topEdge = 0;
    public widthPercentage = 1;
    public height = 0;
    public zIndex = 0;

    public schedule: Schedule;
    public startHour: number;
    public endHour: number;

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
            this.schedule.volunteerId = this.authenticationService.getCurrentUser.personId;
            this.scheduleService.updateSchedule(this.schedule).subscribe( data => {
                this.schedule.volunteerLastName = data.volunteerLastName;
                this.schedule.volunteerFirstName = data.volunteerFirstName;
            });
        }
    }

    public unassignSchedule() {
        if(this.authenticationService.getCurrentUser) {
            this.schedule.volunteerId = null;
            this.schedule.volunteerFirstName = null;
            this.schedule.volunteerLastName = null;
            this.scheduleService.updateSchedule(this.schedule).subscribe(data => {
                this.schedule.volunteerLastName = data.volunteerLastName;
                this.schedule.volunteerFirstName = data.volunteerFirstName;
            });
        }
    }
}