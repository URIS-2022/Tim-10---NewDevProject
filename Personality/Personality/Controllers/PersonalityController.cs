using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personality.Data;
using Personality.Models;
using Personality.ServiceCalls;

namespace Personality.Controllers
{
    [ApiController]
    //[Authorize]
    [Produces("application/json", "application/xml")]
    [Route("api/personality")]
    public class PersonalityController : ControllerBase
    {
        private readonly IPersonalityRepository personalityRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly LinkGenerator linkGenerator;
        private readonly string serviceName = "PersonalityService";
        private readonly Message message = new Message();

        public PersonalityController(IPersonalityRepository personalityRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkGenerator)
        {
            this.personalityRepository = personalityRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.linkGenerator = linkGenerator;
        }
        /// <summary>
        /// Vraća sve ličnosti.
        /// </summary>
        /// <returns>Lista ličnosti</returns>
        /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="204">Nije pronađen ni jedna ličnost u sistemu</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PersonalityDto>> GetPersonalityList()
        {
            List<Entities.Personality> personalities = personalityRepository.GetPersonalityList();
            message.serviceName = serviceName;
            message.method = "GET";

            if (personalities.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.information = "Returned list of Personality";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<PersonalityDto>>(personalities));
        }
        /// <summary>
        /// Vraća ličnost na osnovu identifikatora ličnost.
        /// </summary>
        /// <param name="personalityId">Identifikator licnosti (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Ličnost</returns>
        /// <response code="200">Vraća ličnost koja je pronađena</response>
        /// <response code="204">Ne postoji ličnost sa datim identifikatorom</response>
        [HttpGet("{personalityId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PersonalityDto> GetPersonalityById(Guid personalityId)
        {
            Entities.Personality personality = personalityRepository.GetPersonalityById(personalityId);
            message.serviceName = serviceName;
            message.method = "GET";

            if (personality == null)
            {
                message.information = "Not found";
                message.error = "There is no object of Licnost with identifier: " + personalityId;
                loggerService.CreateMessage(message);
                return NotFound();
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            message.information = personality?.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
            loggerService.CreateMessage(message);
            return mapper.Map<PersonalityDto>(personality);
        }

        /// <summary>
        /// Upisuje ličnost.
        /// </summary>
        /// <param name="licnostDto">Model ličnosti</param>
        /// <returns>Podatke o ličnosti koja je upisana</returns>
        /// <remarks>
        /// Primer zahteva za upis ličnosti \
        /// POST /api/licnost \
        /// {
        ///     "Ime": "Milutina",
        ///     "Prezime": "Milankovic",
        ///     "Funkcija": "Clan"
        /// }
        /// </remarks>
        /// <response code="201">Vraća podatke o upisanoj ličnosti</response>
        /// <response code="500">Postoji neki problem sa upisom</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonalityDto> CreatePersonality([FromBody] PersonalityCreateDto personalityDto)
        {
            message.serviceName = serviceName;
            message.method = "POST";

            try
            {
                Entities.Personality personality = mapper.Map<Entities.Personality>(personalityDto);
                personality = personalityRepository.CreatePersonality(personality);
                personalityRepository.SaveChanges();
                string? location = linkGenerator.GetPathByAction("GetPersonalityById", "Personality", new { personalityId = personality.personalityId });

                message.information = personality?.ToString() + " | Personality Location: " + location;
                loggerService?.CreateMessage(message);
                return Created(location, mapper.Map<PersonalityDto>(personality));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation error!");
            }
        }
        /// <summary>
        /// Briše ličnost na osnovu identifikatora.
        /// </summary>
        /// <param name="licnostId">Identifikator licnosti (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji ličnost sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
        [HttpDelete("{personalityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePersonality(Guid personalityId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";

            try
            {

                if (personalityRepository.GetPersonalityById(personalityId) == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Licnost with identifier: " + personalityId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                Entities.Personality personality = personalityRepository.GetPersonalityById(personalityId);
                personalityRepository.DeletePersonality(personalityId);
                personalityRepository.SaveChanges();
                message.information = "Successfully deleted " + personality.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + personality.ToString());
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Deletion error!");
            }
        }

        /// <summary>
        /// Menja vrednosti obeležja ličnosti.
        /// </summary>
        /// <param name="personalityDTo">Model ličnosti</param>
        /// <returns>Podatke o ličnosti koja je upisana</returns>
        ///     /// <remarks>
        /// Primer zahteva za upis ličnosti \
        /// POST /api/licnost \
        /// {
        ///     "LicnostId": "8d6ab9eb-05d4-4010-6741-08d9f3bac53c",
        ///     "Ime": "Milutin",
        ///     "Prezime": "Milankovic",
        ///     "Funkcija": "Clan"
        /// }
        /// </remarks>
        /// <response code="200">Vraća podatke o izmenjenoj ličnosti</response>
        /// <response code="404">Ne postoji ličnost za koju je pokušana izmena</response>
        /// <response code="500">Postoji neki problem sa izmenom</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonalityDto> UpdatePersonality([FromBody] PersonalityCreateDto personalityDto)
        {
            message.serviceName = serviceName;
            message.method = "PUT";

            try
            {
                Entities.Personality oldPersonality = personalityRepository.GetPersonalityById(personalityDto.personalityId);
                if (oldPersonality == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Licnost with identifier: " + personalityDto.personalityId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                Entities.Personality personality = mapper.Map<Entities.Personality>(personalityDto);
                mapper.Map(personality, oldPersonality);

                personalityRepository.SaveChanges();
                message.information= oldPersonality?.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<PersonalityDto>(oldPersonality));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error editing Personality object");
            }
        }

        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
