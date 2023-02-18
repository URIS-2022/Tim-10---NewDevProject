using AutoMapper;
using Buyer.Data;
using Buyer.Entities;
using Buyer.Models;
using Buyer.ServiceCalls;
using Microsoft.AspNetCore.Mvc;

namespace Buyer.Controllers
{
    [ApiController]
    [Route("api/contactPerson")]
    [Produces("application/json", "application/xml")]
    public class ContactPersonController : ControllerBase
    {
        private readonly IContactPersonRepository contactPersonRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly Message message = new Message();
        private readonly string serviceName = "ContactPersonService";

        public ContactPersonController(IContactPersonRepository contactPersonRepository, LinkGenerator linkGenerator, ILoggerService loggerService, IMapper mapper)
        {
            this.contactPersonRepository = contactPersonRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
            this.mapper = mapper;
        }
        ///<summary>
        ///All contact people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ContactPersonDto>> GetContactPerson()
        {
            List<ContactPerson> contactPeople = contactPersonRepository.GetContactPesron();

            message.ServiceName = serviceName;
            message.Method = "GET";

            if (contactPeople == null || contactPeople.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of contact people";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<ContactPersonDto>>(contactPeople));
        }
        ///<summary>
        ///Find contact person with given ID
        /// </summary>
        /// <param name="contactPersonId">Enter valid Id</param>
        ///<returns></returns>
        [HttpGet("{contactPersonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContactPersonDto> GetContactPersonById(Guid contactPersonId)
        {
            ContactPerson contactPerson = contactPersonRepository.GetContactPersonById(contactPersonId);

            message.ServiceName = serviceName;
            message.Method = "GET";
            if (contactPerson == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of kontakt osoba with identifier: " + contactPersonId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = contactPerson.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ContactPersonDto>(contactPerson));
        }
        ///<summary>
        ///Delete contact person
        ///</summary>
        ///<param name="contactPersonId">Enter valid ID</param>
        ///<returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{contactPersonId}")]
        public IActionResult DeleteContactPerson(Guid contactPersonId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                ContactPerson contactPerson = contactPersonRepository.GetContactPersonById(contactPersonId);

                if (contactPerson == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of kontakt osoba with identifier: " + contactPersonId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                contactPersonRepository.DeleteContactPerson(contactPersonId);
                contactPersonRepository.SaveChanges();
                message.Information = "Successfuly deleted" + contactPersonId;
                return NoContent();
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactPerson"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactPersonDto> CreateContactPerson([FromBody]ContactPersonDto contactPerson)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {

                ContactPerson contactPerson1 = mapper.Map<ContactPerson>(contactPerson);
                ContactPerson cont = contactPersonRepository.CreateContactPerson(contactPerson1);
                contactPersonRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetContactPersonById", "ContactPerson", new { contactactPersonId = cont.contactPersonId });

                message.Information = contactPerson.ToString() + " | contact person location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<ContactPersonDto>(contactPerson));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }
        }
        /// <summary>
        /// Updating contact person
        /// </summary>
        /// <returns></returns>
        /// 
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<ContactPersonConformationDto> UpdateContactPerson(ContactPersonUpdateDto contactPerson)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            var oldPerson = contactPersonRepository.GetContactPersonById(contactPerson.contactPersonId);
            if (oldPerson == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of kontakt osoba with identifier: " + contactPerson.contactPersonId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            ContactPerson newPerson = mapper.Map<ContactPerson>(contactPerson);
            mapper.Map(newPerson, oldPerson);

            contactPersonRepository.SaveChanges();

            message.Information = oldPerson.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ContactPersonConformationDto>(oldPerson));
        }

        ///<summary>
        ///Options for contact person
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
