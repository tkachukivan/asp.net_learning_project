import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Subject } from 'rxjs';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class WebService {

    BASE_URL = 'https://localhost:5001/api';

    private contactsStore: any = [];

    private contactsSubject = new Subject();

    contacts = this.contactsSubject.asObservable();

    constructor(
        private http: HttpClient,
        private sb: MatSnackBar,
        private auth: AuthService,
        private router: Router
    ) { }

    getContacts() {
        this.http
            .get(`${this.BASE_URL}/contacts`, { headers: this.auth.tokenHeader })
            .subscribe(
                (res) => {
                    this.contactsStore = res;
                    this.contactsSubject.next(this.contactsStore);
                },
                () => this.sb.open('unable to get contacts', 'close', { duration: 2000 })
            );
    }

    getContactById(contactId) {
        return this.http
            .get(`${this.BASE_URL}/contacts/${contactId}`, { headers: this.auth.tokenHeader })
    }

    createContact(contact) {
        this.http
            .post(`${this.BASE_URL}/contacts`, contact, { headers: this.auth.tokenHeader })
            .subscribe(
                (res: any) => {
                    this.contactsStore.push(res);
                    this.sb.open('contact was created', 'close', { duration: 2000 });
                    this.router.navigate(['/contact', res.id]);
                },
                () => this.sb.open('unable to create contacts', 'close', { duration: 2000 })
            );
    }

    updateContact(contact, contactId) {
        this.http
            .put(`${this.BASE_URL}/contacts/${contactId}`, contact, { headers: this.auth.tokenHeader })
            .subscribe(
                (res: any) => {
                    this.sb.open('contact was updated', 'close', { duration: 2000 });
                },
                () => this.sb.open('unable to create contacts', 'close', { duration: 2000 })
            );
    }

    removeContact(contactId) {
        this.http
            .delete(`${this.BASE_URL}/contacts/${contactId}`, { headers: this.auth.tokenHeader })
            .subscribe(
                (res: any) => {
                    this.contactsStore = this.contactsStore.filter( contact => contact.id !== contactId);
                    this.contactsSubject.next(this.contactsStore);
                    this.sb.open('contact was deleted', 'close', { duration: 2000 });
                },
                () => this.sb.open('unable to delete contact', 'close', { duration: 2000 })
            );
    }

    getUser() {
        return this.http.get(`${this.BASE_URL}/users/me`, { headers: this.auth.tokenHeader });
    }

    updateUser(user) {
        return this.http.post(`${this.BASE_URL}/users/me`, user, { headers: this.auth.tokenHeader })
            .subscribe(
                (res: any) => {
                    localStorage.setItem(this.auth.NAME_KEY, res.firstName);
                }
            );
    }
}
