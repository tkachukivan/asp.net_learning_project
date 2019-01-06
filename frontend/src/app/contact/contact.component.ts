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
        email = '',
        birthdate = '',
        address = {},
    }: any) {
        // const phonesArray = new FormArray([]);
        const addressFormGroup = new FormGroup({
            country: new FormControl(address.country || ''),
            city: new FormControl(address.city || ''),
            street: new FormControl(address.street || ''),
            building: new FormControl(address.building || ''),
            appartments: new FormControl(address.appartments || ''),
            zipCode: new FormControl(address.zipCode || ''),
        })

        // for (const phone of phones) {
        //     phonesArray.push(
        //         new FormGroup({
        //             number: new FormControl( phone.number, Validators.required ),
        //             type: new FormControl( phone.type, Validators.required )
        //         })
        //     );
        // }
        
        this.form = this.fb.group({
            firstName: [firstName, Validators.required],
            lastName: [lastName, Validators.required],
            email: [email],
            address: addressFormGroup,
            birthdate: birthdate,
            // phones: phonesArray,
        });
    }

    // addNewPhone() {
    //     const phonesFormArray = this.form.get('phones');
        
    //     phonesFormArray.push(
    //         new FormGroup({
    //             number: new FormControl( null, Validators.required ),
    //             type: new FormControl( null, Validators.required )
    //         })
    //     )
    // }

    // removePhone(index) {
    //     const phonesFormArray = this.form.get('phones');
        
    //     phonesFormArray.removeAt(index);
    // }

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
