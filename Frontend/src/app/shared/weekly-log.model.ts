import { Schedule } from "./schedule.model";

export class WeeklyLog {
    public id: string;
    public beneficiaryId: string;
    public beneficiaryFirstName: string;
    public beneficiaryLastName: string;
    public startTime: string;
    public dayOfWeek: {
        value: number,
        name: string
    };
    public Schedule: Schedule | null;
    public duration: number;
}