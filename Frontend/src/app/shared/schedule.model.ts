export class Schedule {
    public minutes: number; // Duration
    public dayOfWeek: string;
    public beneficiaryFirstName: string;
    public beneficiaryLastName: string;
    public startTime: { hours: number, minutes: number};
    public weeklyLogId: string;
}