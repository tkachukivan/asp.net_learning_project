import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

    loginData = {
        email: '',
        password: ''
    };

    constructor(
        private auth: AuthService
    ) { }

    ngOnInit() {
    }

    login() {
        this.auth.login(this.loginData);
        this.loginData.email = '';
        this.loginData.password = '';
    }

}
