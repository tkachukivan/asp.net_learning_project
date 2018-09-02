import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
    form;

    constructor(
        private fb: FormBuilder,
        private auth: AuthService,
    ) {
        this.form = fb.group({
            userName: ['', Validators.required],
            email: ['', [Validators.required, emailValidator()]],
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required],
        },
            {
                validator: [
                    matchingFields('password', 'confirmPassword')
                ]
            }
        );
    }

    ngOnInit() {
    }

    onSubmit() {
        if (this.form.valid) {
            this.auth.register({ ...this.form.value });
            this.form.reset();
            this.form.markAsUntouched();
            Object.keys(this.form.controls).forEach((name) => {
                const control = this.form.controls[name];
                control.setErrors(null);
            });
        }
    }
}

function matchingFields(field1, field2) {
    return (form) => {
        if (form.controls[field1].value !== form.controls[field2].value) {
            return {
                mismatchedFields: true,
            };
        }
    };
}

function emailValidator() {
    return (control) => {
        const reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; // tslint:disable-line
        return reg.test(control.value) ? null : { invalidEmail: true };
    };
}
