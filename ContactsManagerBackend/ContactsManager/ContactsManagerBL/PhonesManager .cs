using System;
using System.Collections.Generic;
using AutoMapper;
using ContactsManagerBL.Models;
using ContactsManagerDAL;
using PhoneDAL = ContactsManagerDAL.Models.Phone;


namespace ContactsManagerBL
{
    public class PhonesManager
    {
        private PhonesProvider PhonesProvider { get; } = new PhonesProvider();

        public List<Phone> GetPhones(Guid contactId) => Mapper.Map<List<Phone>>(PhonesProvider.GetPhones(contactId));

        public Phone GetPhone(Guid contactId, Guid Id) => Mapper.Map<Phone>(PhonesProvider.GetPhone(contactId, Id));

        public Phone CreatePhone(Guid contactId, Phone phone)
        {
            if (
                phone == null ||
                phone.Number.Number == null
                )
            {
                throw new ArgumentNullException();
            }

            var createdPhone = PhonesProvider.CreatePhone(contactId, Mapper.Map<PhoneDAL>(phone));

            return Mapper.Map<Phone>(createdPhone);
        }

        public void UpdatePhone(Guid contactId, Guid Id, Phone phone)
        {
            if (
                phone == null ||
                phone.Number.Number == null
                )
            {
                throw new ArgumentNullException();
            }

            PhonesProvider.UpdatePhone(contactId, Id, Mapper.Map<PhoneDAL>(phone));
        }

        public void RemovePhone(Guid contactId, Guid Id) => PhonesProvider.RemovePhone(contactId, Id);
    }
}
