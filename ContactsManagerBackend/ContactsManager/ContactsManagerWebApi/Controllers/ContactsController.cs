using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ContactsManagerBL;

namespace ContactsManagerWebApi.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactsManager _contactManager = new ContactsManager();

        // GET: Contacts
        public IHttpActionResult Get()
        {
            return Ok(_contactManager.GetContacts());
        }
    }
}