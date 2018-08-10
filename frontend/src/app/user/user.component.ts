import { Component, OnInit } from '@angular/core';
import { WebService } from '../web.service';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
})
export class UserComponent implements OnInit {

    userData = {
        firstName: '',
        lastName: ''
    };

    constructor(
        private webService: WebService
    ) { }

    ngOnInit() {
        this.webService.getUser()
            .subscribe(
                (res: any) => {
                    this.userData.firstName = res.firstName;
                    this.userData.lastName = res.lastName;
                }
            );
    }

    update() {
        this.webService.updateUser({ ...this.userData });
    }

}
