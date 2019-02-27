import { PhoneTypes } from '../enums/phoneTypes';

export class PhoneNumber {
    countryCode = '';
    number = '';
}

export class Phone {
    public id = '';
    public localNumber: PhoneNumber = new PhoneNumber();
    public phoneType: PhoneTypes = PhoneTypes.Home;
    public isNew = true;
    public deleted = false;
}
