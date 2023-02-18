using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicBidding.Data;
using PublicBidding.Entities;
using PublicBidding.Models;
using PublicBidding.Services;

namespace PublicBidding.Controllers
{
	//Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
	[ApiController]
	[Route("api/statusOfPublicBidding")]
	[Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
	public class StatusOfPublicBiddingController : ControllerBase
	{
		private readonly IStatusOfPublicBiddingRepository statusOfPublicBiddingRepository;
		private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
		private readonly IMapper mapper;
		private readonly ILoggerService loggerService;
		private readonly string serviceName = "PublicBiddingService";
		private readonly Message message = new Message();

		//Pomocu dependency injection-a dodajemo potrebne zavisnosti
		public StatusOfPublicBiddingController(IStatusOfPublicBiddingRepository statusOfPublicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
		{
			this.statusOfPublicBiddingRepository = statusOfPublicBiddingRepository;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
			this.loggerService = loggerService;
		}

		/// <summary>
		/// Get all statuses of public bidding
		/// </summary>
		/// <returns>List statuses of public bidding</returns>
		/// <response code="200">List statuses of public bidding</response>
		/// <response code="404">Not found any status of public bidding</response>
		[HttpGet]
		[HttpHead] // Returns only the header in the response
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<StatusOfPublicBiddingDto>> GetStatusesOfPublicBidding()
		{
			List<StatusOfPublicBidding> statusesOfPublicBidding = statusOfPublicBiddingRepository.GetStatusesOfPublicBidding();
			message.ServiceName = serviceName;
			message.Method = "GET";
			//if no licitation is found, return status 204(NoContent)
			if (statusesOfPublicBidding == null || statusesOfPublicBidding.Count == 0)
			{
				message.Information = "No content";
				message.Error = "There is no content in database!";
				loggerService.CreateMessage(message);
				return NoContent();
			}

			message.Information = "Returned list of StatusOfPublicBidding";
			loggerService.CreateMessage(message);
			// If a licitation is found, return the status 200 and the list of found status
			return Ok(mapper.Map<List<StatusOfPublicBiddingDto>>(statusesOfPublicBidding));
		}

		/// <summary>
		/// Get status of public bidding by id
		/// </summary>
		/// // <param name="statusOfPublicBiddingId">Status of public bidding id</param>
		/// <returns>Requested status of public bidding</returns>
		/// <response code="200">Get requested status of public bidding</response>
		/// <response code="404">Requested status of public bidding not found</response>
		[HttpGet("{statusOfPublicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<StatusOfPublicBiddingDto> GetStatusOfPublicBidding(Guid statusOfPublicBiddingId)
		{
			StatusOfPublicBidding? statusOfPublicBidding = statusOfPublicBiddingRepository.GetStatusOfPublicBiddingById(statusOfPublicBiddingId);
			message.ServiceName = serviceName;
			message.Method = "GET";
			if (statusOfPublicBidding == null)
			{
				message.Information = "Not found";
				message.Error = "There is no object of StatusOfPublicBidding with identifier: " + statusOfPublicBiddingId;
				loggerService.CreateMessage(message);
				return NotFound();
			}
			message.Information = statusOfPublicBidding.ToString();
			loggerService.CreateMessage(message);
			return Ok(mapper.Map<StatusOfPublicBiddingDto>(statusOfPublicBidding));
		}

		/// <summary>
		/// Create status of public bidding
		/// </summary>
		/// /// <param name="statusOfPublicBiddingDto">Status of public bidding model</param>
		/// <returns>Confirmation of the created status of public bidding</returns>
		/// <remarks>
		/// Example of a request to create a new status of public bidding \
		/// POST /api/statusOfPublicBidding \
		/// {   \
		///    "statusOfPublicBiddingName": "New status" \
		/// }
		/// </remarks>
		/// <response code="201">Get created status of public bidding</response>
		/// <response code="500">An error occurred on the server while creating the status of public bidding</response>
		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<StatusOfPublicBiddingConfirmationDto> CreateStatusOfPublicBidding([FromBody] StatusOfPublicBiddingCreationDto statusOfPublicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "POST";
			try
			{
				StatusOfPublicBidding status = mapper.Map<StatusOfPublicBidding>(statusOfPublicBiddingDto);
				StatusOfPublicBiddingConfirmationDto s = statusOfPublicBiddingRepository.CreateStatusOfPublicBidding(status);
				statusOfPublicBiddingRepository.SaveChanges(); //Perzistiramo promene
														  //generisati identifikator novokreiranog resursa
				string? location = linkGenerator.GetPathByAction("GetStatusOfPublicBidding", "StatusOfPublicBidding", new { statusOfPublicBiddingId = s.statusOfPublicBiddingId });
				message.Information = statusOfPublicBiddingDto.ToString() + " | StatusOfPublicBidding location: " + location;
				loggerService.CreateMessage(message);
				return Created(location, mapper.Map<StatusOfPublicBiddingConfirmationDto>(s));
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
		/// Update status of public bidding
		/// </summary>
		/// <param name="statusOfPublicBiddingDto">Status of public bidding model to be updated</param>
		/// <returns>Confirmation of updated status of public bidding</returns>
		/// <response code="200">Get updated status of public bidding</response>
		/// <response code="400">Status of public bidding to be updated was not found</response>
		/// <response code="500">An error occurred while updating the status of public bidding</response>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<StatusOfPublicBiddingConfirmationDto> UpdateStatusOfPublicBidding(StatusOfPublicBiddingUpdateDto statusOfPublicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "PUT";
			try
			{
				var starije = statusOfPublicBiddingRepository.GetStatusOfPublicBiddingById(statusOfPublicBiddingDto.statusOfPublicBiddingId);
				if (starije == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of StatusOfPublicBidding with identifier: " + statusOfPublicBiddingDto.statusOfPublicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound(); 
				}
				StatusOfPublicBidding statusOfPublicBidding = mapper.Map<StatusOfPublicBidding>(statusOfPublicBiddingDto);
				mapper.Map(statusOfPublicBidding, starije);
				statusOfPublicBiddingRepository.SaveChanges(); //Perzistiramo promene
				message.Information = starije.ToString();
				loggerService.CreateMessage(message);
				return Ok(mapper.Map<StatusOfPublicBiddingConfirmationDto>(starije));
			}
			catch (Exception ex)
			{
				message.Information = "Server error";
				message.Error = ex.Message;
				loggerService.CreateMessage(message);
				return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
			}
		}

		/// <summary>
		/// Delete status of public bidding
		/// </summary>
		/// <param name="statusOfPublicBiddingId">Status of public bidding id</param>
		/// <returns>Status 204 (NoContent)</returns>
		/// <response code="204">status of public bidding deleted</response>
		/// <response code="404">No status of public bidding found to delete</response>
		/// <response code="500">>An error occurred on the server while deleting the status of public bidding</response>
		[HttpDelete("{statusOfPublicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteStatusOfPublicBidding(Guid statusOfPublicBiddingId)
		{
			message.ServiceName = serviceName;
			message.Method = "DELETE";
			try
			{
				StatusOfPublicBidding statusOfPublicBidding = statusOfPublicBiddingRepository.GetStatusOfPublicBiddingById(statusOfPublicBiddingId);
				if (statusOfPublicBidding == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of StatusOfPublicBidding with identifier: " + statusOfPublicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				statusOfPublicBiddingRepository.DeleteStatusOfPublicBidding(statusOfPublicBiddingId);
				statusOfPublicBiddingRepository.SaveChanges(); //Perzistiramo promene
				message.Information = "Successfully deleted " + statusOfPublicBidding.ToString();
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
		/// Vraca opcije dostupne za rad sa statusima nadmetanja.
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetStatusOfPublicBiddingOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
