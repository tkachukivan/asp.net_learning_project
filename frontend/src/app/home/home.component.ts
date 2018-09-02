import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { WebService } from '../web.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    constructor(
        private authService: AuthService,
        private webServise: WebService
    ) { }

    ngOnInit() {
        if (this.authService.authenticate && !this.authService.name) {
            this.webServise.getUser()
                .subscribe(
                    (res: any) => {
                        this.authService.name = res.userName;
                    }
                )
        }
    }
}
