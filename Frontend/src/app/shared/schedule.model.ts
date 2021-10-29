import { WeeklyLog } from "./weekly-log.model";

export class Schedule {
    public id: string;
    public weeklyLogId: string;
    public volunteerId: string | null;
    public volunteerFirstName: string | null;
    public volunteerLastName: string | null;
    public date: Date;
    public weeklyLog: WeeklyLog;
}