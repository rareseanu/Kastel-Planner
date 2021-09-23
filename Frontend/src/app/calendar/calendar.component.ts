import { ChangeDetectorRef, Component, ComponentFactoryResolver, ComponentRef, QueryList, Type, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { EventComponent } from './event/event.component';

import { ScheduleService } from '../shared/schedule.service';
import { Schedule } from '../shared/schedule.model';

@Component({
    templateUrl: 'calendar.component.html',
    styleUrls: ['./calendar.component.css']
})

export class CalendarComponent {
    public static days: Record<string, number> = {
        "Monday": 1,
        "Tuesday": 2,
        "Wednesday": 3,
        "Thursday": 4,
        "Friday": 5,
        "Saturday": 6,
        "Sunday": 7
    };
    events: Schedule[];

    public currentWeekMonday: Date;
    public currentWeekSunday: Date;
    public currentDay: Date;
    public currentMonth: string;
    public currentYear: any;

    public startHour = 8;

    @ViewChildren('day', {read: ViewContainerRef}) eventContainers: QueryList<ViewContainerRef>;
    components: ComponentRef<any>[][];
    componentClass = EventComponent;
    lastIndex0: ComponentRef<any>;
    lastIndex0Pos: number;

    constructor(private resolver: ComponentFactoryResolver, private cdr: ChangeDetectorRef, private scheduleService: ScheduleService) { 
        this.loadWeek(new Date());
    }

    loadWeek(day: Date) {
        this.currentDay = day;
        this.currentWeekMonday = this.getMonday(this.currentDay);
        this.currentWeekSunday = new Date(this.currentWeekMonday.getTime() + (6 * 24 * 60 * 60 * 1000));
        this.currentMonth = this.currentDay.toLocaleString('default', { month: 'long' });
        this.currentYear = this.currentDay.getFullYear();
    }

    loadNextWeek() {
        this.loadWeek(new Date(this.currentDay.getTime() + (7 * 24 * 60 * 60 * 1000)));
        this.loadEventComponents();
    }

    loadPreviousWeek() {
        this.loadWeek(new Date(this.currentDay.getTime() + (-7 * 24 * 60 * 60 * 1000)));
        this.loadEventComponents();
    }

    getHour(offset: number) {
        let hour = offset + this.startHour;
        if(hour > 24) {
            hour = hour - 24;
        }
        if(hour < 10) {
            return `0${hour}:00`;
        } else {
            return `${hour}:00`;
        }
    }

    getDayOfMonth(days: number) {
        let newDate = new Date();
        newDate.setDate(this.currentWeekMonday.getDate() + days);
        return newDate.getDate();
    }

    getMonday(d: Date) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6:1); 
        return new Date(d.setDate(diff));
    }

    ngAfterViewInit() {
        this.components = [];
        for(var day = 0; day < 7; ++day) {
            this.components.push([]);
        }
        this.scheduleService.getAllSchedules(this.currentWeekMonday, this.currentWeekSunday).subscribe(data => {
            this.events = data;
            this.setupEventComponents();
        });
    }

    loadEventComponents() {
        for(let day = 0; day < 7; ++day) {
            for(let component of this.components[day]) {
                component.destroy();
            }
            this.components[day] = [];
        }
        this.scheduleService.getAllSchedules(this.currentWeekMonday, this.currentWeekSunday).subscribe(data => {
            this.events = data;
            this.setupEventComponents();
        });
    }

    setupEventComponents() {
        const factory = this.resolver.resolveComponentFactory(this.componentClass);
        let index = 0;
        let containers = this.eventContainers.toArray();
        for(let event of this.events) {
            const component = containers[CalendarComponent.days[event.dayOfWeek]-1].createComponent(factory);

            let parsedHour = new Date(`2021-01-21 ${event.startTime}`);
            let startHour = parsedHour.getHours() + parsedHour.getMinutes() / 60;
            let endHour = startHour + event.minutes / 60;
            let duration = endHour - startHour;

            if(this.startHour < startHour) {
                component.instance.topEdge = 41 * (startHour - this.startHour);
            } else {
                component.instance.topEdge = 41 * (24 - Math.abs(this.startHour - startHour));
            }
            component.instance.height = 40 * duration;
            component.instance.title = "event" + index++;
            component.instance.startHour = startHour;
            component.instance.endHour = endHour;
            component.instance.schedule = event;

            this.components[CalendarComponent.days[event.dayOfWeek]].push(component);
            this.cdr.detectChanges();
        }
        for(let componentArray of this.components) {
            let temp = 0;
            let baseLeft = 1 / (componentArray.length);
    
            this.lastIndex0 = componentArray[0];
            this.lastIndex0Pos = 0;
            
            for(let c of componentArray) {
                
                let endHourFirst = componentArray[this.lastIndex0Pos].instance.endHour * 60;
                let startHourCurrent = componentArray[temp].instance.startHour * 60;
    
                if(componentArray.length == 1) {
                    c.instance.widthPercentage = 1;
                } else {
                    if(temp != this.components.length) {
                        c.instance.widthPercentage = baseLeft * 1.7;
                    } else {
                        c.instance.widthPercentage = baseLeft;
                    }
                }
    
                if(startHourCurrent >= endHourFirst) {
                    c.instance.leftEdge = 0;
                    c.instance.widthPercentage = 1;
                    c.instance.zIndex = componentArray[0].instance.zIndex;
                    this.lastIndex0 =  c.instance;
                    this.lastIndex0Pos = temp;
    
                    let tempBaseLeft = 1 / (temp - 1);
                    for(let i = 0; i < this.lastIndex0Pos - 1; ++i) {
                        componentArray[i].instance.widthPercentage = tempBaseLeft * 1.7;
                        componentArray[i].instance.leftEdge = 1 / (temp - 1) * i;
                        componentArray[i].instance.zIndex = i;
                        
                    }
                    componentArray[this.lastIndex0Pos - 2].instance.widthPercentage = tempBaseLeft;
    
                    if(componentArray[temp-1].instance.zIndex == 0) {
                        componentArray[temp-1].instance.widthPercentage = componentArray[this.lastIndex0Pos].instance.widthPercentage;
                    } else {
                        componentArray[temp-1].instance.widthPercentage = tempBaseLeft;
                    }
                }
                ++temp;
    
            }
        }

        this.cdr.detectChanges();
    }
}