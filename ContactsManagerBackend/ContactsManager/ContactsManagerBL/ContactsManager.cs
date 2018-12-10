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

        public Contact GetContact(Guid Id) => Mapper.Map<Contact>(ContactsProvider.GetContact(Id));

        public Contact CreateContact(Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null ||
                contact.Birthdate == null
                )
            {
                throw new ArgumentException();
            }

            var createdContact = ContactsProvider.CreateContact(Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(createdContact);
        }

        public Contact UpdateContact(Guid Id, Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null ||
                contact.Birthdate == null
                )
            {
                throw new ArgumentException();
            }

            var updatedContact = ContactsProvider.UpdateContact(Id, Mapper.Map<ContactDAL>(contact));

            return Mapper.Map<Contact>(updatedContact);
        }
        public void RemoveContact(Guid Id) => ContactsProvider.RemoveContact(Id);
    }
}
