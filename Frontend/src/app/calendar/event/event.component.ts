import { Component } from '@angular/core';

@Component({
    selector: 'event',
    templateUrl: 'event.component.html',
    styleUrls: ['./event.component.css']
})
export class EventComponent {
    public title = '';
    public leftEdge = 0;
    public topEdge = 0;
    public widthPercentage = 1;
    public height = 0;
    public zIndex = 0;

    public beneficiaryFirstName: string;
    public beneficiaryLastName: string;
    public startHour: number;
    public endHour: number;

    renderHour(hour: number) {
        let hours = Math.floor(hour);
        let minutes = (hour - hours) * 60;
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
}