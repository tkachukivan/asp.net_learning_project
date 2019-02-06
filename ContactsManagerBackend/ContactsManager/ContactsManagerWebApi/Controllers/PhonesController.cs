using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ContactsManagerBL;
using ContactsManagerWebApi.Models;
using PhoneBL = ContactsManagerBL.Models.Phone;

namespace ContactsManagerWebApi.Controllers
{
    [RoutePrefix("api/contacts/{contactId}/phones")]
    public class PhonesController : ApiController
    {
        private PhonesManager PhonesManager { get; } = new PhonesManager();

        [Route("")]
        public IHttpActionResult Get(Guid contactId)
        {
            return Ok<List<Phone>>(Mapper.Map<List<Phone>>(PhonesManager.GetPhones(contactId)));
        }

        [Route("{Id}")]
        public IHttpActionResult Get(Guid contactId, Guid Id)
        {
            var phone = PhonesManager.GetPhone(contactId, Id);

            if(phone == null)
            {
                return NotFound();
            }
            else
            {
                return Ok<Phone>(Mapper.Map<Phone>(phone));
            }
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Guid contactId, [FromBody] Phone phone)
        {
            if (
                phone == null ||
                phone.Number.Number == null
                )
            {
                return BadRequest();
            }

            var createdPhone = PhonesManager.CreatePhone(contactId, Mapper.Map<PhoneBL>(phone));

            return Ok<Phone>(Mapper.Map<Phone>(createdPhone));
        }

        [Route("{Id}")]
        [HttpPut]
        public IHttpActionResult Put(Guid contactId, Guid Id, [FromBody] Phone phone)
        {
            if (
                phone == null ||
                phone.Number.Number == null
                )
            {
                return BadRequest();
            }

            PhonesManager.UpdatePhone(contactId, Id, Mapper.Map<PhoneBL>(phone));

            return Ok();
        }

        [Route("{Id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid ContactId, Guid Id)
        {
            PhonesManager.RemovePhone(ContactId, Id);

            return Ok();
        }
    }
}