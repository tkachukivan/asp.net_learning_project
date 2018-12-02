using System;
using System.Collections.Generic;
using AutoMapper;
using ContactsManagerDAL;
using ContactDAL = ContactsManagerDAL.Contact;


namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider ContactsProvider { get; } = new ContactsProvider();

        public List<Contact> GetContacts() => Mapper.Map<List<Contact>>(ContactsProvider.GetContacts());

        public Contact GetContactById(Guid Id) => Mapper.Map<Contact>(ContactsProvider.GetContactById(Id));

        public Contact CreateContact(Contact contact)
        {
            var createdContact = ContactsProvider.CreateContact(Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(createdContact);
        }

        public Contact UpdateContact(Guid Id, Contact contact)
        {
            var updatedContact = ContactsProvider.UpdateContact(Id, Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(updatedContact);
        }
        public void RemoveContact(Guid Id) => ContactsProvider.RemoveContact(Id);
    }
}
