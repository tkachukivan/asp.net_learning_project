using AutoMapper;
using ContactsManagerDAL;
using System.Collections.Generic;

namespace ContactsManagerBL
{
    public class ContactsManager
    {
        private ContactsProvider ContactsProvider { get; } = new ContactsProvider();

        public List<ContactModel> GetContacts()
        {
            var contacts = MappingDto.Mapper.Map<List<ContactDto>, List<ContactModel>>(ContactsProvider.GetContacts());

            return contacts;
        }
    }
}
