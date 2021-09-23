export class WeeklyLog {
    public id: string;
    public beneficiaryId: string;
    public startTime: string;
    public dayOfWeek: {
        value: number,
        name: string
    };
}