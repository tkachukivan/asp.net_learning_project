using ContactsManagerDAL;
using System.Collections.Generic;

namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider ContactsProvider { get; } = new ContactsProvider();

        public List<Contact> GetContacts()
        {
            return ContactsProvider.GetContacts();
        }
    }
}
