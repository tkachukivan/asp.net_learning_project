import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    BASE_URL = 'http://localhost:59561/api/auth';
    NAME_KEY = 'name';
    TOKEN_KEY = 'token';

    constructor(
        private http: HttpClient,
        private router: Router,
    ) { }

    get name() {
        return localStorage.getItem(this.NAME_KEY);
    }

    set name(value) {
        localStorage.setItem(this.NAME_KEY, value);
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

    login(loginData) {
        loginData.grant_type = 'password'
        const httpParams = new HttpParams()
            .append('username', loginData.userName)
            .append('password', loginData.password)
            .append('grant_type', 'password');

        this.http.post(
            `${this.BASE_URL}/login`,
            httpParams.toString(),
            {
                headers: new HttpHeaders()
                    .set('Content-Type', 'application/x-www-form-urlencoded'),
            }
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

    authenticate({ access_token, userName }: any) {
        if (!access_token) {
            return;
        }

        localStorage.setItem(this.TOKEN_KEY, access_token);
        if (userName) {
            localStorage.setItem(this.NAME_KEY, userName);
        }
        

        this.router.navigate(['/']);
    }
}
