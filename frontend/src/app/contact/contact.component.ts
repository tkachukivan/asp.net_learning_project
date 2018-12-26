import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ContactsService } from '../services/contacts.service';
import { ActivatedRoute } from '@angular/router';
import { Contact } from '../models/contact.model';
import { PhoneTypes } from '../enums/phoneTypes';
import { Phone } from '../models/phone.model';

@Component({
    selector: 'app-contact',
    templateUrl: './contact.component.html',
})
export class ContactComponent implements OnInit {
    @ViewChild('contactForm') contactForm: NgForm;;
    private contactId: string;
    private isContactNew: boolean = true;
    private maxDatepickerDate: Date = new Date();
    private contact = new Contact();
    private phoneTypes = PhoneTypes;

    constructor(
        private contactsService: ContactsService,
        private router: ActivatedRoute,
    ) {}

    ngOnInit() {
        this.contactId = this.router.snapshot.params.contactId;

        if (this.contactId) {
            this.isContactNew = false;
            this.contactsService.getContactById(this.contactId)
                .subscribe(
                    (contact: Contact) => {
                        this.contact = contact;
                    }
                );
        }
    }

    addNewPhone() {
        this.contact.phones.push(new Phone());
    }

    removePhone(index) {
        if (this.contact.phones[index].isNew) {
            this.contact.phones.splice(index, 1);
        } else {
            this.contact.phones[index].deleted = true;
        }
    }

    onSubmit() {
        if (this.contactForm.valid) {
            if (this.isContactNew) {
                this.contactsService.createContact(this.contact);
            } else {
                this.contactsService.updateContact(this.contact, this.contactId)
            }
        }
    }
}
