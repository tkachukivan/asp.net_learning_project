using AutoMapper;
using ContactsManagerDAL;
using ContactDAL = ContactsManagerDAL.Contact;
using System.Collections.Generic;

namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider ContactsProvider { get; } = new ContactsProvider();

        public List<Contact> GetContacts() => Mapper.Map<List<Contact>>(ContactsProvider.GetContacts());
    }
}
