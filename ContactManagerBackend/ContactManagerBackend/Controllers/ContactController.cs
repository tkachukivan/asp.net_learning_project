using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace ContactManagerBackend.Controllers
{
    [Authorize]
    public class ContactsController : ApiController
    {
        readonly ApiContext context = new ApiContext();

        public ContactsController()
        {
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var CurrentUserId = GetCurrentUserId();

            if (CurrentUserId == null)
            {
                return NotFound();
            }

            var UserContacts = context.Contacts.Where(contact => contact.OwnerId == CurrentUserId)
                                                .Include(c => c.Phones)
                                                .Include(c => c.Address)
                                                .ToList();

            return Ok(UserContacts);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Models.Contact Contact = context.Contacts.Include(c => c.Phones)
                                                    .Include(c => c.Address)
                                                    .SingleOrDefault(contact => contact.Id == id);

            if (Contact == null)
            {
                return NotFound();
            }

            return Ok(Contact);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Models.Contact contact)
        {
            var CurrentUserId = GetCurrentUserId();

            if (CurrentUserId == null)
            {
                return NotFound();
            }

           contact.OwnerId = CurrentUserId;
            var dbContact = context.Contacts.Add(contact);

            context.SaveChanges();

            return Ok(dbContact);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]Models.Contact contactData)
        {
            Models.Contact ContactToUpdate = context.Contacts.Include(c => c.Phones)
                                                            .Include(c => c.Address)
                                                            .SingleOrDefault(contact => contact.Id == id);

            if (ContactToUpdate == null)
            {
                return NotFound();
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

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Models.Contact ContactToDelete = context.Contacts.Include(c => c.Phones)
                                                            .Include(c => c.Address)
                                                            .SingleOrDefault(contact => contact.Id == id);

            if (ContactToDelete == null)
            {
                return NotFound();
            }

            context.Contacts.Remove(ContactToDelete);

            context.SaveChanges();

            return Ok();
        }

        private Guid GetCurrentUserId()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return default(Guid);
            }

            var id = identity.Claims
                             .FirstOrDefault(çlaim => çlaim.Type == "id")?.Value;

            return Guid.Parse(id);
        }
    }
}