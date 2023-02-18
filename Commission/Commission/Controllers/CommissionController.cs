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
    [Route("api/commission")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionRepository commissionRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly string serviceName = "Commission";
        private readonly Message message = new Message();

        public CommissionController(ICommissionRepository commissionRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.commissionRepository = commissionRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        /// <summary>
        /// Returns all commissions
        /// </summary>
        /// <returns>A list of commissions</returns>
        /// <response code="200">Returns a list of commissions</response>
        /// <response code="204">There are no commissions</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CommissionDto>> GetAllCommissions(Guid? presidentId)
        {
            var commission = commissionRepository.GetAllCommissions(presidentId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (commission == null || commission.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                return NoContent();
            }
            message.information = "Returned list of commissions";
            return Ok(mapper.Map<List<CommissionDto>>(commission));
        }
        /// <summary>
        /// Returns a commission with a passed ID
        /// </summary>
        /// <param name="commissionId">Commission ID</param>
        /// <returns>Commission</returns>
        /// <response code="200">A found commission</response>
        /// <response code="204">There is no commission with the passed ID</response>
        [HttpGet("{commissionId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommissionDto> GetCommission(Guid commissionId)
        {
            var commission = commissionRepository.GetCommissionById(commissionId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (commission == null)
            {
                message.information = "Not found";
                message.error = "There is no object with identifier: " + commissionId;
                return NotFound();
            }
            message.information = commission.ToString();
            return Ok(mapper.Map<CommissionDto>(commission));
        }
        /// <summary>
        /// Adds a commission
        /// </summary>
        /// <param name="commissionDto">Model of a commission</param>
        /// <returns>Data about the commission</returns>
        /// <remarks>
        /// EXAMPLE \
        /// POST /api/commission \
        /// {
        ///     "commissionId" : "4A1ABAEE-8984-4682-82FB-D3E832207C72",
        ///     "nameOfCommission" : "First",
        ///     "presidentId" : "F5468F83-D3AF-49DF-8136-7D5323CAD68B"
        /// }
        /// </remarks>
        /// <response code="201">Returns data about the added commission</response>
        /// <response code="500">An error occurred during adding</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CommissionDto> CreateCommission([FromBody] CommissionDto commission)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {
                CommissionEntity _commission = mapper.Map<CommissionEntity>(commission);
                CommissionDto confirmation = commissionRepository.CreateCommission(_commission);
                commissionRepository.SaveChanges();

                string? location = linkGenerator.GetPathByAction("GetCommission", "Commission", new { commissionId = confirmation.commissionId });
                message.information = commission.ToString() + " | Commission location: " + location;
                return Created(location, mapper.Map<CommissionDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during creating");
            }
        }
        /// <summary>
        /// Updates a Commission
        /// </summary>
        /// <param name="commissionDto">Model of a commission</param>
        /// <returns>Data about a commission</returns>
        ///     /// <remarks>
        /// EXAMPLE \
        /// POST /api/commission \
        /// {
        ///     "commissionId" : "4A1ABAEE-8984-4682-82FB-D3E832207C72",
        ///     "nameOfCommission" : "First",
        ///     "presidentId" : "F5468F83-D3AF-49DF-8136-7D5323CAD68B"
        /// }
        /// </remarks>
        /// <response code="200">Returns data about the updated commission</response>
        /// <response code="404">There is no commission you tried to update</response>
        /// <response code="500">An error occurred during updating</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CommissionDto> UpdateCommission(CommissionDto commission)
        {
            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                var old = commissionRepository.GetCommissionById(commission.commissionId);
                if (old == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + commission.commissionId;
                    return NotFound();
                }
                CommissionEntity neww = mapper.Map<CommissionEntity>(commission);
                mapper.Map(neww, old);
                commissionRepository.SaveChanges();
                message.information = old.ToString();
                return Ok(mapper.Map<CommissionDto>(old));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }
        /// <summary>
        /// Deletes a commission with the passed ID
        /// </summary>
        /// <param name="commissionId">Commission ID</param>
        /// <returns>string</returns>
        /// <response code="204">Returns a message about a successful deletion</response>
        /// <response code="404">There is no commission with the passed ID</response>
        /// <response code="500">An error occurred during deleting</response>
        [HttpDelete("{commissionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCommission(Guid commissionId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                var commission = commissionRepository.GetCommissionById(commissionId);
                if (commission == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + commissionId;
                    return NotFound();
                }
                commissionRepository.DeleteCommission(commissionId);
                commissionRepository.SaveChanges();
                message.information = "Successfully deleted " + commission.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + commission.ToString());
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
        public IActionResult GetCommissionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
