import { PhoneTypes } from "../enums/phoneTypes";

export class Phone {
    public number: string = '';
    public type: PhoneTypes = PhoneTypes.Home;
    public isNew: boolean = true;
    public deleted: boolean = false;
}