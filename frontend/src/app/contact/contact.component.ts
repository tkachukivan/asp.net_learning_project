import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup, FormArray } from '@angular/forms';
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

    phoneTypes = [
        {
            value: 0,
            viewValue: 'Home'
        },
        {
            value: 1,
            viewValue: 'Mobile'
        },
        {
            value: 2,
            viewValue: 'Other'
        },
    ]

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
        address = {},
        birthdate = '',
        phones = [],
    }: any) {
        const phonesArray = new FormArray([]);
        const addressFormGroup = new FormGroup({
            country: new FormControl(address.country || '', Validators.required),
            city: new FormControl(address.city || '', Validators.required),
            street: new FormControl(address.street || '', Validators.required),
            building: new FormControl(address.building || '', Validators.required),
            appartments: new FormControl(address.appartments || '', Validators.required),
            zipCode: new FormControl(address.zipCode || '', Validators.required),
        })

        for (const phone of phones) {
            phonesArray.push(
                new FormGroup({
                    number: new FormControl( phone.number, Validators.required ),
                    type: new FormControl( phone.type, Validators.required )
                })
            );
        }
        
        this.form = this.fb.group({
            firstName: [firstName, Validators.required],
            lastName: [lastName, Validators.required],
            middleName: middleName,
            email: [email, [Validators.required]],
            address: addressFormGroup,
            birthdate: birthdate,
            phones: phonesArray,
        });
    }

    addNewPhone() {
        const phonesFormArray = this.form.get('phones');
        
        phonesFormArray.push(
            new FormGroup({
                number: new FormControl( null, Validators.required ),
                type: new FormControl( null, Validators.required )
            })
        )
    }

    removePhone(index) {
        const phonesFormArray = this.form.get('phones');
        
        phonesFormArray.removeAt(index);
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
