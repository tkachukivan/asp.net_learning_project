import { Component, OnInit } from '@angular/core';
import { WebService } from '../web.service';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
})
export class ContactsListComponent implements OnInit {

  constructor(
    private webService: WebService
  ) {}

  ngOnInit() {
    this.webService.getContacts();
  }

  removeContact(contactId) {
    this.webService.removeContact(contactId);
  }
}
