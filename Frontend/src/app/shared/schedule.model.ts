export class Schedule {
    public id: string;
    public minutes: number; // Duration
    public dayOfWeek: string;
    public beneficiaryFirstName: string;
    public beneficiaryLastName: string;
    public startTime: { hours: number, minutes: number};
    public weeklyLogId: string;
    public volunteerId: string | null;
    public volunteerFirstName: string | null;
    public volunteerLastName: string | null;
    public date: Date;
}