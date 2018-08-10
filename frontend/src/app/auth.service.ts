import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    BASE_URL = 'https://localhost:5001/auth';
    NAME_KEY = 'name';
    TOKEN_KEY = 'token';

    constructor(
        private http: HttpClient,
        private router: Router
    ) { }

    get name() {
        return localStorage.getItem(this.NAME_KEY);
    }

    get isAuthenticated() {
        return !!localStorage.getItem(this.TOKEN_KEY);
    }

    get tokenHeader() {
        const header = new HttpHeaders(
            {
                'Authorization': `Bearer ${localStorage.getItem(this.TOKEN_KEY)}`
            }
        );

        return header;
    }

    register(user) {
        delete user.comfirmPassword;
        this.http.post(`${this.BASE_URL}/register`, user)
            .subscribe(
                (res) => {
                    this.authenticate(res);
                }
            );
    }

    login(loginDate) {
        this.http.post(
            `${this.BASE_URL}/login`,
            loginDate
        )
        .subscribe( (res) => {
            this.authenticate(res);
        });
    }

    logout() {
        localStorage.removeItem(this.TOKEN_KEY);
        localStorage.removeItem(this.NAME_KEY);
        this.router.navigate(['/login']);
    }

    authenticate({ token, firstName }: any) {
        if (!token) {
            return;
        }

        localStorage.setItem(this.TOKEN_KEY, token);
        localStorage.setItem(this.NAME_KEY, firstName);

        this.router.navigate(['/']);
    }
}
