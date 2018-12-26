import { Address } from "./address.model";
import { Phone } from "./phone.model";

export class Contact {
    public id: string = '';
    public firstName: string = '';
    public lastName: string = '';
    public email: string = '';
    public birthdate: Date;
    public address: Address = new Address();
    public phones: Array<Phone> = [];
}