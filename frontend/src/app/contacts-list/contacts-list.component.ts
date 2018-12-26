import { Component, OnInit } from '@angular/core';
import { ContactsService } from '../services/contacts.service';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
})
export class ContactsListComponent implements OnInit {

  constructor(
    private contactsService: ContactsService
  ) {}

  ngOnInit() {
    this.contactsService.getContacts();
  }

  removeContact(contactId: string) {
    this.contactsService.removeContact(contactId);
  }
}
