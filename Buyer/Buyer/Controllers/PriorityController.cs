using AutoMapper;
using Buyer.Data;
using Buyer.Entities;
using Buyer.Models;
using Buyer.ServiceCalls;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Buyer.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [ApiController]
    [Route("api/priorities")]
    [Produces("application/json", "application/xml")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityRepository priorityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly Message message = new Message();
        private readonly string serviceName = "BuyerService";


        /// <summary>
        ///
        ///</summary>
        /// <param name="priorityRepository"></param>
        /// <param name="loggerService"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        
        public PriorityController(IPriorityRepository priorityRepository, LinkGenerator linkGenerator, ILoggerService loggerService, IMapper mapper)
        {
            this.priorityRepository = priorityRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns a list of priorities that already exist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        public ActionResult<List<BuyerPriorityDto>> GetPriorityList()
        {
            List<PriorityModel> priorities = priorityRepository.GetPriority();
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (priorities == null || priorities.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "List of priorities";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<BuyerPriorityDto>>(priorities));
        }

        ///<summary>
        ///Returns priority with given ID
        /// </summary>
        /// <param name="priorityId">Enter valid id</param>
        /// <returns></returns>
        [HttpGet("{priorityId}")]
        public ActionResult<BuyerPriorityDto> GetPriorityById(Guid priorityId)
        {
            PriorityModel priorityModel = priorityRepository.GetPriorityById(priorityId);

            message.ServiceName = serviceName;
            message.Method = "GET";

            if (priorityModel == null)
            {
                message.Information = "Not found.";
                message.Error = "There is no object with given Id in the database";
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = priorityModel.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<BuyerPriorityDto>(priorityModel));
        }
        ///<summary>
        ///Removing priorities
        /// </summary>
        /// <param name="priorityId">Enter valid ID</param>
        /// <returns></returns>
        [HttpDelete("{priorityId}")]
        public IActionResult DeletePriority(Guid priorityId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                PriorityModel priorityModel = priorityRepository.GetPriorityById(priorityId);
                if (priorityModel == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of prioritet with identifier: " + priorityId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                priorityRepository.DeletePriority(priorityId);
                priorityRepository.SaveChanges();
                message.Information = "Successfully deleted " + priorityId.ToString();
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
        ///<summary>
        ///Adding new priority
        /// </summary>
        /// <param name="priority">Enter priority corectly</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BuyerPriorityDto> CreatePriority([FromBody]BuyerPriorityDto priority)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                PriorityModel prior = mapper.Map<PriorityModel>(priority);
                PriorityModel priorityCreate = priorityRepository.CreatePriority(prior);
                priorityRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPriorityById", "Priority", new { priorityId = prior.priorityId });

                message.Information = priority.ToString() + " | Priority location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<PriorityModel>(priorityCreate));
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PriorityConformationDto> UpdatePriority(PriorityUpdateDto priority)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var oldPriority = priorityRepository.GetPriorityById(priority.priorityId);
                if (oldPriority == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of prioritet with identifier: " + priority.priorityId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                PriorityModel newPriority = mapper.Map<PriorityModel>(priority);
                mapper.Map(newPriority, oldPriority);

                priorityRepository.SaveChanges();

                message.Information = oldPriority.ToString();
                loggerService.CreateMessage(message);

                return Ok(mapper.Map<PriorityConformationDto>(oldPriority));
            }
            catch (Exception e)
            {
                message.Information = "Server error";
                message.Error = e.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
            }
        }
        ///<summary>
        ///Options for the buyer
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetPrioritetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
