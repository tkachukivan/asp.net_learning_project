using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerBackend.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactsController : Controller
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

            if (CurrentUser == null)
            {
                return NotFound("No user");
            }

            var UserContacts = context.Contacts.Where( contact => contact.OwnerId == CurrentUser.Id)
                                                .Include( c => c.Phones)
                                                .Include(c => c.Address)
                                                .ToList();

            return Ok(UserContacts);
        }

        [Authorize]
        [HttpGet("{contactId}")]
        public ActionResult GetById(Guid contactId)
        {
            Models.Contact Contact = context.Contacts.Include(c => c.Phones)
                                                    .Include(c => c.Address)
                                                    .SingleOrDefault(contact => contact.Id == contactId);

            if (Contact == null)
            {
                return NotFound("Contact Not Found");
            }

            return Ok(Contact);
        }

        [Authorize]
        [HttpPost()]
        public ActionResult Post([FromBody] Models.Contact contact)
        {
            Models.User CurrentUser = GetSecureUser();

            if (CurrentUser == null)
            {
                return NotFound("No user to add contact");
            }

            contact.OwnerId = CurrentUser.Id;
            Models.Contact dbContact = context.Contacts.Add(contact).Entity;

            context.SaveChanges();

            return Ok(dbContact);
        }

        [Authorize]
        [HttpPut("{contactId}")]
        public ActionResult Put(Guid contactId, [FromBody] Models.Contact contactData)
        {
            Models.Contact ContactToUpdate = context.Contacts.Include(c => c.Phones)
                                                            .Include(c => c.Address)
                                                            .SingleOrDefault( contact => contact.Id == contactId);

            if (ContactToUpdate == null) {
                return NotFound("Contact Not Found");
            }

            ContactToUpdate.FirstName = contactData.FirstName ?? ContactToUpdate.FirstName;
            ContactToUpdate.LastName = contactData.LastName ?? ContactToUpdate.LastName;
            ContactToUpdate.MiddleName = contactData.MiddleName ?? ContactToUpdate.MiddleName;

            if (ContactToUpdate.Address != null && contactData.Address != null)
            {
                ContactToUpdate.Address.Country = contactData.Address.Country ?? ContactToUpdate.Address.Country;
                ContactToUpdate.Address.City = contactData.Address.City ?? ContactToUpdate.Address.City;
                ContactToUpdate.Address.Street = contactData.Address.Street ?? ContactToUpdate.Address.Street;
                ContactToUpdate.Address.ZipCode = contactData.Address.ZipCode ?? ContactToUpdate.Address.ZipCode;
                ContactToUpdate.Address.Building = contactData.Address.Building ?? ContactToUpdate.Address.Building;
                ContactToUpdate.Address.Appartments = contactData.Address.Appartments ?? ContactToUpdate.Address.Appartments;
            }

            ContactToUpdate.Email = contactData.Email ?? ContactToUpdate.Email;
            ContactToUpdate.Phones = contactData.Phones ?? ContactToUpdate.Phones;
            
            if (contactData.Birthdate != null)
            {
                ContactToUpdate.Birthdate = contactData.Birthdate;
            }
            
            context.SaveChanges();

            return Ok(ContactToUpdate);
        }

        [Authorize]
        [HttpDelete("{contactId}")]
        public ActionResult Delete(Guid contactId)
        {
            Models.Contact ContactToDelete = context.Contacts.Include(c => c.Phones)
                                                            .Include(c => c.Address)
                                                            .SingleOrDefault(contact => contact.Id == contactId);

            if (ContactToDelete == null)
            {
                return NotFound("Contact Not Found");
            }

            context.Contacts.Remove(ContactToDelete);

            context.SaveChanges();

            return Ok();
        }


        Models.User GetSecureUser()
        {
            var id = HttpContext.User.Claims.First().Value;

            return context.Users.SingleOrDefault((u) => u.Id == Guid.Parse(id));
        }
    }
}