using System;
using System.Collections.Generic;
using AutoMapper;
using ContactsManagerBL.Models;
using ContactsManagerDAL;
using ContactDAL = ContactsManagerDAL.Models.Contact;


namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider ContactsProvider { get; } = new ContactsProvider();

        public List<Contact> GetContacts() => Mapper.Map<List<Contact>>(ContactsProvider.GetContacts());

        public Contact GetContact(Guid Id) => Mapper.Map<Contact>(ContactsProvider.GetContact(Id));

        public Contact CreateContact(Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null
                )
            {
                throw new ArgumentNullException();
            }

            var createdContact = ContactsProvider.CreateContact(Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(createdContact);
        }

        public Contact UpdateContact(Guid Id, Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null 
                )
            {
                throw new ArgumentNullException();
            }

            var updatedContact = ContactsProvider.UpdateContact(Id, Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(updatedContact);
        }
        public void RemoveContact(Guid Id) => ContactsProvider.RemoveContact(Id);
    }
}
