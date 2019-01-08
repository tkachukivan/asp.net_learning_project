import { PhoneTypes } from "../enums/phoneTypes";

export class Phone {
    public id: string = '';
    public number: PhoneNumber = new PhoneNumber();
    public phoneType: PhoneTypes = PhoneTypes.Home;
    public isNew: boolean = true;
    public deleted: boolean = false;
}

export class PhoneNumber {
    countryCode: string = '';
    number: string = '';
}