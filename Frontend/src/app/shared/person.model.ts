import { Label } from "./label";
import { Role } from "./role.model";

export class Person {
    public id: '';
    public firstName: '';
    public lastName: '';
    public phoneNumber: '';
    public isActive: boolean;
    public roles: Role[];
    public labels: Label[];
}