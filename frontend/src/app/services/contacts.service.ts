import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Subject, AsyncSubject, forkJoin, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Contact } from '../models/contact.model';
import { Phone } from '../models/phone.model';

@Injectable({
    providedIn: 'root'
})
export class ContactsService {

    private BASE_URL = 'http://localhost:8888/api';

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
        const subject = new AsyncSubject();
        const contact = this.http.get(`${this.BASE_URL}/contacts/${contactId}`);
        const phones = this.http.get(`${this.BASE_URL}/contacts/${contactId}/phones`);

        forkJoin([contact, phones])
            .subscribe((res: any) => {
                const contactModel: Contact = res[0];
                contactModel.phones = res[1];

                subject.next(contactModel);
                subject.complete();
            });

        return subject;
    }

    createContact(contact: Contact) {
        const contactPhones = [...contact.phones];
        delete contact.phones;

        this.http
            .post(`${this.BASE_URL}/contacts`, contact)
            .pipe(
                mergeMap((res: Contact) => {
                    return this.createPhonesRequests(res.id, contactPhones);
                })
            ).subscribe(
                () => {
                    this.sb.open('contact was created', 'close', { duration: 2000 });
                    this.router.navigate(['/']);
                },
                () => this.sb.open('unable to create contacts', 'close', { duration: 2000 })
            );
    }

    updateContact(contact: Contact, contactId: string) {
        const contactPhones = [...contact.phones];
        delete contact.phones;

        const updateContactRequest = this.http
            .put(`${this.BASE_URL}/contacts/${contactId}`, contact);
        forkJoin([
            updateContactRequest,
            this.createPhonesRequests(contact.id, contactPhones)
        ])
            .subscribe(
                (res: any) => {
                    this.sb.open('contact was updated', 'close', { duration: 2000 });
                    this.router.navigate(['/']);
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

    createPhonesRequests(contactId: string, phones: Array<Phone>) {
        const requests = phones.reduce((acc, phone) => {
            if (phone.isNew) {
                acc.push(this.http.post(`${this.BASE_URL}/contacts/${contactId}/phones`, phone));
            } else if (phone.deleted) {
                acc.push(this.http.delete(`${this.BASE_URL}/contacts/${contactId}/phones/${phone.id}`));
            } else {
                acc.push(this.http.put(`${this.BASE_URL}/contacts/${contactId}/phones/${phone.id}`, phone));
            }

            return acc;
        }, []);

        if (requests.length === 0) {
            return of([]);
        } else {
            return forkJoin(requests);
        }
    }
}
