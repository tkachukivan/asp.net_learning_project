using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerBackend.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        readonly ApiContext context;

        public ContactsController(ApiContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet()]
        public ActionResult Get()
        {
            Models.User CurrentUser = GetSecureUser();

            var UserContacts = context.Contacts.Where( contact => contact.OwnerId == CurrentUser.Id);

            return Ok(UserContacts);
        }

        [Authorize]
        [HttpGet("{contactId}")]
        public ActionResult GetById(string contactId)
        {
            Models.Contact Contact = context.Contacts.SingleOrDefault(contact => contact.Id == contactId);

            return Ok(Contact);
        }

        [Authorize]
        [HttpPost()]
        public ActionResult Post([FromBody] Models.Contact contact)
        {
            Models.User CurrentUser = GetSecureUser();
            contact.OwnerId = CurrentUser.Id;
            Models.Contact dbContact = context.Contacts.Add(contact).Entity;

            context.SaveChanges();

            return Ok(dbContact);
        }

        [Authorize]
        [HttpPut("{contactId}")]
        public ActionResult Put(string contactId, [FromBody] Models.Contact contactData)
        {
            Models.User CurrentUser = GetSecureUser();

            Models.Contact contactToUpdate = context.Contacts.SingleOrDefault( contact => contact.Id == contactId);

            contactToUpdate.FirstName = contactData.FirstName ?? contactToUpdate.FirstName;
            contactToUpdate.LastName = contactData.LastName ?? contactToUpdate.LastName;
            contactToUpdate.MiddleName = contactData.MiddleName ?? contactToUpdate.MiddleName;
            contactToUpdate.Address = contactData.Address ?? contactToUpdate.Address;
            contactToUpdate.Email = contactData.Email ?? contactToUpdate.Email;
            contactToUpdate.HomePhone = contactData.HomePhone ?? contactToUpdate.HomePhone;
            contactToUpdate.MobilePhone = contactData.MobilePhone ?? contactToUpdate.MobilePhone;
            contactToUpdate.Birthdate = contactData.Birthdate ?? contactToUpdate.Birthdate;

            context.SaveChanges();

            return Ok(contactToUpdate);
        }

        [Authorize]
        [HttpDelete("{contactId}")]
        public ActionResult Delete(string contactId)
        {
            Models.Contact contactToDelete = context.Contacts.SingleOrDefault(contact => contact.Id == contactId);

            context.Contacts.Remove(contactToDelete);

            context.SaveChanges();

            return Ok();
        }


        Models.User GetSecureUser()
        {
            var id = HttpContext.User.Claims.First().Value;

            return context.Users.SingleOrDefault((u) => u.Id == id);
        }
    }
}