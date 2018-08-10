import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Subject } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class WebService {

    BASE_URL = 'https://localhost:5001/api';

    private messagesStore: any = [];

    private messageSubject = new Subject();

    messages = this.messageSubject.asObservable();

    constructor(
        private http: HttpClient,
        private sb: MatSnackBar,
        private auth: AuthService,
    ) {
        this.getMessages();
    }

    getMessages(user = '') {
        user = user ? `/${user}` : '';
        this.http
            .get(`${this.BASE_URL}/messages${user}`)
            .subscribe(
                (res) => {
                    this.messagesStore = res;
                    this.messageSubject.next(this.messagesStore);
                },
                () => this.sb.open('unable to get messages', 'close', { duration: 2000 })
            );
    }

    async postMessage(message) {
        try {
            const newMessage = await this.http
                .post(`${this.BASE_URL}/messages`, message)
                .toPromise();

            this.messagesStore.push(newMessage);
            this.messageSubject.next(this.messagesStore);
        } catch (err) {
            this.sb.open('unable to post message', 'close', { duration: 2000 });
        }
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
