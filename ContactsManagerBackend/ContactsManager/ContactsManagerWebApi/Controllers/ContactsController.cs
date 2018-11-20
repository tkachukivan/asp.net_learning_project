using System.Web.Http;
using ContactsManagerBL;

namespace ContactsManagerWebApi.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactsManager ContactsManager { get; } = new ContactsManager();

        // GET: Contacts
        public IHttpActionResult Get()
        {
            return Ok(ContactsManager.GetContacts());
        }
    }
}