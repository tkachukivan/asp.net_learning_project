using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ContactsManagerBL;
using ContactsManagerWebApi.Models;
using ContactBL = ContactsManagerBL.Models.Contact;

namespace ContactsManagerWebApi.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactsManager ContactsManager { get; } = new ContactsManager();

        // GET: Contacts
        public IHttpActionResult Get()
        {
            return Ok<List<Contact>>(Mapper.Map<List<Contact>>(ContactsManager.GetContacts()));
        }

        public IHttpActionResult Get(Guid Id)
        {
            var contact = ContactsManager.GetContact(Id);

            if(contact == null)
            {
                return NotFound();
            }
            else
            {
                return Ok<Contact>(Mapper.Map<Contact>(contact));
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null ||
                contact.Address == null ||
                contact.Address.Country == null ||
                contact.Address.City == null ||
                contact.Address.Street == null
                )
            {
                return BadRequest();
            }

            var createdContact = ContactsManager.CreateContact(Mapper.Map<ContactBL>(contact));

            return Ok<Contact>(Mapper.Map<Contact>(createdContact));
        }

        [HttpPut]
        public IHttpActionResult Put(Guid Id, [FromBody] Contact contact)
        {
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null ||
                contact.Address == null ||
                contact.Address.Country == null ||
                contact.Address.City == null ||
                contact.Address.Street == null
                )
            {
                return BadRequest();
            }

            var updatedContact = ContactsManager.UpdateContact(Id, Mapper.Map<ContactBL>(contact));

            return Ok<Contact>(Mapper.Map<Contact>(updatedContact));
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid Id)
        {
            ContactsManager.RemoveContact(Id);

            return Ok();
        }
    }
}