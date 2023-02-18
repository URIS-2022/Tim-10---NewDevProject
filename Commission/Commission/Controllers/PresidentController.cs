using AutoMapper;
using Azure;
using Commission.Data;
using Commission.Entities;
using Commission.Models;
using Commission.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commission.Controllers
{
    [Route("api/president")]
    [ApiController]
    public class PresidentController : ControllerBase
    {
        private readonly IPresidentRepository presidentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly string serviceName = "Commission";
        private readonly Message message = new Message();
        private readonly IPersonalityService personalityService;


        public PresidentController(IPresidentRepository presidentRepository, LinkGenerator linkGenerator, IMapper mapper, IPersonalityService personalityService)
        {
            this.presidentRepository = presidentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personalityService = personalityService;
        }
        /// <summary>
        /// Returns all presidents
        /// </summary>
        /// <returns>A list of presidents</returns>
        /// <response code="200">Returns a list of presidents</response>
        /// <response code="204">There are no presidents</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PresidentDto>>? GetAllPresidents()
        {

            message.serviceName = serviceName;
            message.method = "GET";
            List<PresidentEntity> president = presidentRepository.GetAllPresidents();
            if (president == null || president.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                return NoContent();
            }
            try
            {
                foreach (PresidentEntity p in president)
                {
                    PersonalityDto personality = personalityService.GetPersonality(p.personalityId).Result;
                    if (personality != null)
                    {
                        p.personalityDto = personality;
                    }
                }
            }
            catch
            {
                return default;
            }

            message.information = "Returned list of presidents";
            return Ok(president);

        }
        /// <summary>
        /// Returns a president with the passed ID
        /// </summary>
        /// <param name="presidentId">President ID</param>
        /// <returns>President</returns>
        /// <response code="200">Returns a president with the passed ID</response>
        /// <response code="204">There is no president with the passed ID</response>
        [HttpGet("{presidentId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PresidentDto> GetPresident(Guid presidentId)
        {
            PresidentEntity president = presidentRepository.GetPresidentById(presidentId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (president == null)
            {
                message.information = "Not found";
                message.error = "There is no object with identifier: " + presidentId;
                return NotFound();
            }
            PresidentDto presidentDto = mapper.Map<PresidentDto>(president);
            presidentDto.personality = personalityService.GetPersonality(president.presidentId).Result;
            message.information = president.ToString();
            return Ok(presidentDto);

        }
        /// <summary>
        /// Adds a president
        /// </summary>
        /// <param name="presidentDto">Model of a president</param>
        /// <returns>Data about the president</returns>
        /// <remarks>
        /// EXAMPLE \
        /// POST /api/president \
        /// {
        ///      "presidentId" : "F5468F83-D3AF-49DF-8136-7D5323CAD68B"
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Returns data about the added president</response>
        /// <response code="500">An error occurred durring adding</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PresidentDto> CreatePresident([FromBody] PresidentDto president)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {
                PresidentEntity _president = mapper.Map<PresidentEntity>(president);
                PresidentDto confirmation = presidentRepository.CreatePresident(_president);
                presidentRepository.SaveChanges();

                string? location = linkGenerator.GetPathByAction("GetPresident", "President", new { presidentId = confirmation.presidentId });
                message.information = president.ToString() + " | President location: " + location;

                return Created(location, mapper.Map<PresidentDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred durring adding");
            }
        }

        /// <summary>
        /// Updates a president.
        /// </summary>
        /// <param name="memberDto">Model of a president</param>
        /// <returns>Data about the member</returns>
        ///     /// <remarks>
        /// EXAMPLE \
        /// POST /api/president \
        /// {
        ///     "memberId": "54a107-684d0d5-205-f724-08d9f3dcf86e",
        ///     "commisionId": "54a107-684d0d5-205-f724-08d9f3dcf86e"
        /// }
        /// </remarks>
        /// <response code="200">Returns data about the updated president</response>
        /// <response code="404">There is no president which you tried to update</response>
        /// <response code="500">An error occurred during updating</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PresidentDto> UpdatePresident(PresidentDto president)
        {
            message.serviceName = serviceName;
            message.method = "PUT";

            try
            {
                var old = presidentRepository.GetPresidentById(president.presidentId);
                if (old == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + president.presidentId;
                    return NotFound();
                }
                PresidentEntity neww = mapper.Map<PresidentEntity>(president);
                mapper.Map(neww, old);
                presidentRepository.SaveChanges();
                message.information = old.ToString();
                return Ok(mapper.Map<PresidentDto>(president));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }

        /// <summary>
        /// Deletes a president with the passed ID
        /// </summary>
        /// <param name="presidentId">President ID</param>
        /// <returns>string</returns>
        /// <response code="204">Returns a message about a successful deletion</response>
        /// <response code="404">There is no president with the passed ID</response>
        /// <response code="500">An error occurred during deleting</response>
        [HttpDelete("{presidentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePresident(Guid presidentId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                PresidentEntity president = presidentRepository.GetPresidentById(presidentId);
                if (president == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + presidentId;
                    return NotFound();
                }
                presidentRepository.DeletePresident(presidentId);
                presidentRepository.SaveChanges();
                message.information = "Successfully deleted " + president.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + president.ToString());
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting");
            }
        }

        /// <summary>
        /// The methods you can use
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPredsednikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
