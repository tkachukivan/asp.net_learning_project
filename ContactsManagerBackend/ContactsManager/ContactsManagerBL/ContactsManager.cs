using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsManagerDAL;

namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider _contactProvider = new ContactsProvider();

        public List<Contact> GetContacts()
        {
            return _contactProvider.GetContacts();
        }
    }
}
