using AuthorizedPerson.Data;
using AuthorizedPerson.Entities;
using AuthorizedPerson.Models;
using AuthorizedPerson.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizedPerson.Controllers
{
    [ApiController]
    [Route("api/authorizedPerson")]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly ILoggerService loggerService;
        private readonly IMapper mapper;
        private readonly string serviceName = "AuthorizedPersonService";
        private readonly Message message = new Message();


        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, ILoggerService loggerService)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
            this.loggerService = loggerService;
            this.mapper = mapper;
        }
        [HttpGet]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<List<AuthorizedPersonDto>> GetAuthorizedPeople()
        {
            List<AuthorizedPersonModel> people = authorizedPersonRepository.GetAuthorizedPeople();
            message.ServiceName = serviceName;
            if(people == null || people.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of ovlascena lica";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<AuthorizedPersonDto>>(people));
        }
        [HttpGet("{authorizedId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AuthorizedPersonDto> GetAuthorizedPersonById(Guid APID)
        {
            AuthorizedPersonModel people = authorizedPersonRepository.GetAuthorizedPersonById(APID);

            message.ServiceName = serviceName;
            message.Method = "GET";

            if(people == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of authorized person with identifier: " + APID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = people.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<AuthorizedPersonDto>(people));
        }

        /// <summary>
        /// Delete authorized person 
        /// </summary>
        /// <param name="authorizedPersonId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{authorizedPersonId}")]
        public IActionResult DeleteAuthorizedPerson(Guid APID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";


            try
            {
                AuthorizedPersonModel person = authorizedPersonRepository.GetAuthorizedPersonById(APID);
                if (person == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of authorized person with identifier: " + APID;
                    loggerService.CreateMessage(message);
                    return NotFound();

                }

                authorizedPersonRepository.DeleteAuthorizedPerson(APID);
                authorizedPersonRepository.SaveChanges();

                message.Information = "Successfully deleted " + APID.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + APID.ToString());

            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");

            }
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonDto> CreateAuthorizedPerson([FromBody] AuthorizedPersonDto authorizedPerson)
        {
            message.ServiceName=serviceName;
            message.Method = "POST";

            try
            {
                AuthorizedPersonModel person =  mapper.Map<AuthorizedPersonModel>(authorizedPerson);
                person.ducumentNumber = authorizedPerson.documentNumber;
                AuthorizedPersonModel createPerson = authorizedPersonRepository.CreateAuthorizedPerson(person);
                authorizedPersonRepository.SaveChanges();

                

                return Ok(mapper.Map<AuthorizedPersonDto>(createPerson));
            }
            catch (Exception e)
            {
                message.Information = "Server error";
                message.Error = e.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);

            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonConformationDto> UpdateAuthorizedPerson(AuthorizedPersonUpdateDto authorizedPerson)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            try
            {
                var oldPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPerson.authorizedPersonId);
                if (oldPerson == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of authorized person with identifier: " + authorizedPerson.authorizedPersonId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                AuthorizedPersonModel authorized = mapper.Map<AuthorizedPersonModel>(authorizedPerson);
                authorized.ducumentNumber = authorizedPerson.documentNumber;
                mapper.Map(authorized, oldPerson);
                authorizedPersonRepository.SaveChanges();
                message.Information = oldPerson.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<AuthorizedPersonConformationDto>(oldPerson));

            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
