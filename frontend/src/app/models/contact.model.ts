import { Address } from './address.model';
import { Phone } from './phone.model';

export class Contact {
    public id = '';
    public firstName = '';
    public lastName = '';
    public email = '';
    public birthdate: Date;
    public address: Address = new Address();
    public phones: Array<Phone> = [];
}
