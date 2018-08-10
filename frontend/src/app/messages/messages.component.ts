import { Component, OnInit } from '@angular/core';
import { WebService } from '../web.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    templateUrl: './messages.template.html',
    selector: 'app-messages'
})

export class MessagesComponent implements OnInit {
    constructor(
        private webService: WebService,
        private route: ActivatedRoute
    ) {}

    ngOnInit() {
        this.webService.getMessages(this.route.snapshot.params.name);
    }
}
