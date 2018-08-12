import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { WebService } from '../web.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-contact',
    templateUrl: './contact.component.html',
})
export class ContactComponent implements OnInit {
    form;
    contactId;
    isContactNew = true;
    maxDatepickerDate = new Date();

    constructor(
        private fb: FormBuilder,
        private webService: WebService,
        private router: ActivatedRoute,
    ) {
        this.fb = fb;
        this.router = router;
    }

    ngOnInit() {
        this.contactId = this.router.snapshot.params.contactId;
        let contact = {};
        if (this.contactId) {
            this.isContactNew = false;
            this.webService.getContactById(this.contactId)
                .subscribe(
                    (contact) => {
                        this.createForm(contact);
                    }
                );
        } else {
            this.createForm(contact);
        }
    }

    createForm({
        firstName = '',
        lastName = '',
        middleName = '',
        email = '',
        address = '',
        birthdate = '',
        mobilePhone = '',
        homePhone = '',
    }: any) {
        this.form = this.fb.group({
            firstName: [firstName, Validators.required],
            lastName: [lastName, Validators.required],
            middleName: middleName,
            email: [email, [Validators.required]],
            address: address,
            birthdate: birthdate,
            mobilePhone: [mobilePhone, [Validators.required]],
            homePhone: homePhone,
        });
    }

    onSubmit() {
        if (this.form.valid) {
            if (this.isContactNew) {
                this.webService.createContact({ ...this.form.value });
            } else {
                this.webService.updateContact({ ...this.form.value }, this.contactId)
            }
        }
    }
}
