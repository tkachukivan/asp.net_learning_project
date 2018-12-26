import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { Contact } from '../models/contact.model';

@Injectable({
    providedIn: 'root'
})
export class ContactsService {

    private BASE_URL: string = 'http://localhost:8888/api';

    private contactsStore: Array<Contact> = [];

    private contactsSubject: Subject<Contact[]> = new Subject();

    public contacts = this.contactsSubject.asObservable();

    constructor(
        private http: HttpClient,
        private sb: MatSnackBar,
        private router: Router
    ) { }

    getContacts() {
        this.http
            .get(`${this.BASE_URL}/contacts`)
            .subscribe(
                (res: Array<Contact>) => {
                    this.contactsStore = res;
                    this.contactsSubject.next(this.contactsStore);
                },
                () => this.sb.open('unable to get contacts', 'close', { duration: 2000 })
            );
    }

    getContactById(contactId: string) {
        return this.http
            .get(`${this.BASE_URL}/contacts/${contactId}`)
    }

    createContact(contact: Contact) {
        this.http
            .post(`${this.BASE_URL}/contacts`, contact)
            .subscribe(
                (res: any) => {
                    this.sb.open('contact was created', 'close', { duration: 2000 });
                    this.router.navigate(['/']);
                },
                () => this.sb.open('unable to create contacts', 'close', { duration: 2000 })
            );
    }

    updateContact(contact: Contact, contactId: string) {
        this.http
            .put(`${this.BASE_URL}/contacts/${contactId}`, contact)
            .subscribe(
                (res: any) => {
                    this.sb.open('contact was updated', 'close', { duration: 2000 });
                },
                () => this.sb.open('unable to create contacts', 'close', { duration: 2000 })
            );
    }

    removeContact(contactId: string) {
        this.http
            .delete(`${this.BASE_URL}/contacts/${contactId}`)
            .subscribe(
                () => {
                    this.contactsStore = this.contactsStore.filter( contact => contact.id !== contactId);
                    this.contactsSubject.next(this.contactsStore);
                    this.sb.open('contact was deleted', 'close', { duration: 2000 });
                },
                () => this.sb.open('unable to delete contact', 'close', { duration: 2000 })
            );
    }
}
