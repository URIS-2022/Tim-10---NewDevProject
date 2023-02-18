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
	[Route("api/licitation")]
	[Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate

	public class LicitationController : ControllerBase
	{
		private readonly ILicitationRepository licitationRepository;
		private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
		private readonly IMapper mapper;
		private readonly ILoggerService loggerService;
		private readonly string serviceName = "PublicBiddingService";
		private readonly Message message = new Message();

		//Pomocu dependency injection-a dodajemo potrebne zavisnosti
		public LicitationController(ILicitationRepository licitationRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
		{
			this.licitationRepository = licitationRepository;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
			this.loggerService = loggerService;
		}

		/// <summary>
		/// Get all licitations
		/// </summary>
		/// <returns> List licitations</returns>
		/// <response code="200">List licitations</response>
		/// <response code="404">Not found any licitation</response>
		[HttpGet]
		[HttpHead] // Returns only the header in the response
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<LicitationDto>> GetLicitation()
		{
			List<Licitation> licitations = licitationRepository.GetLicitations();
			message.ServiceName = serviceName;
			message.Method = "GET";
			//if no licitation is found, return status 204(NoContent)
			if (licitations == null || licitations.Count == 0)
			{
				message.Information = "No content";
				message.Error = "There is no content in database!";
				loggerService.CreateMessage(message);
				return NoContent();
			}

			message.Information = "Returned list of Licitation";
			loggerService.CreateMessage(message);
			// If a licitation is found, return the status 200 and the list of found licitation
			return Ok(mapper.Map<List<LicitationDto>>(licitations));
		}

		/// <summary>
		/// Get licitation by id
		/// </summary>
		/// // <param name="licitationId">Licitation id</param>
		/// <returns>Requested licitation</returns>
		/// <response code="200">Get requested licitation</response>
		/// <response code="404">Requested licitation not found</response>
		[HttpGet("{licitationId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<LicitationDto> GetLicitations(Guid licitationId)
		{
			Licitation? licitation = licitationRepository.GetLicitationById(licitationId);
			message.ServiceName = serviceName;
			message.Method = "GET";
			if (licitation == null)
			{
				message.Information = "Not found";
				message.Error = "There is no object of Licitation with identifier: " + licitationId;
				loggerService.CreateMessage(message);
				return NotFound();
			}
			message.Information = licitation.ToString();
			loggerService.CreateMessage(message);
			return Ok(mapper.Map<LicitationDto>(licitation));
		}

		/// <summary>
		/// Create licitation
		/// </summary>
		/// /// <param name="licitationDto">Licitation model</param>
		/// <returns>Confirmation of the created licitation.</returns>
		/// <remarks>
		/// Example of a request to create a new licitation \
		/// POST /api/licitation \
		/// {   \
		///    "number": 3, \
		///    "year": 2023, \
		///    "date": "2023-02-19T00:00:00", \
		///    "restrictions": 1, \
		///    "priceDifference": 300, \
		///    "publicBiddingId": "208a48a5-371c-4f9d-ac23-18bb176ff8f3", \
		///    "deadlineForSubmissionOfApplications": "2023-02-17T00:00:00" \
		/// }
		/// </remarks>
		/// <response code="201">Get created licitation</response>
		/// <response code="500">An error occurred on the server while creating the licitation</response>
		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<LicitationConfirmationDto> CreateLicitation([FromBody] LicitationCreationDto licitationDto)
		{
			message.ServiceName = serviceName;
			message.Method = "POST";
			try
			{
				Licitation licitation = mapper.Map<Licitation>(licitationDto);
				LicitationConfirmationDto l = licitationRepository.CreateLicitation(licitation);
				licitationRepository.SaveChanges(); //Perzistiramo promene
													//generisati identifikator novokreiranog resursa
				string? location = linkGenerator.GetPathByAction("GetLicitation", "Licitation", new { licitationId = l.licitationId });
				message.Information = licitationDto.ToString() + " | Licitation location: " + location;
				loggerService.CreateMessage(message);
				return Created(location, mapper.Map<LicitationConfirmationDto>(l));
			}
			catch (Exception ex)
			{
				message.Information = ex.Message;
				message.Error = ex.Message;
				loggerService.CreateMessage(message);
				return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
			}
		}

		/// <summary>
		/// Update licitation
		/// </summary>
		/// <param name="licitationDto">Licitation model to be updated</param>
		/// <returns>Confirmation of updated licitation</returns>
		/// <response code="200">Get updated licitation</response>
		/// <response code="400">The licitation to be updated was not found</response>
		/// <response code="500">An error occurred while updating the licitation</response>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<LicitationConfirmationDto> UpdateLicitation(LicitationUpdateDto licitationDto)
		{
			message.ServiceName = serviceName;
			message.Method = "PUT";
			try
			{
				var starije = licitationRepository.GetLicitationById(licitationDto.licitationId);
				if (starije == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of Licitation with identifier: " + licitationDto.licitationId;
					loggerService.CreateMessage(message);
					return NotFound(); 
				}
				Licitation licitation = mapper.Map<Licitation>(licitationDto);
				mapper.Map(licitation, starije);
				licitationRepository.SaveChanges(); //Perzistiramo promene
				message.Information = starije.ToString();
				loggerService.CreateMessage(message);
				return Ok(mapper.Map<LicitationConfirmationDto>(starije));
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
		/// Delete licitation
		/// </summary>
		/// <param name="licitationId">Licitation id</param>
		/// <returns>Status 204 (NoContent)</returns>
		/// <response code="204">Licitation deleted</response>
		/// <response code="404">No licitation found to delete</response>
		/// <response code="500">An error occurred on the server while deleting the licitation</response>
		[HttpDelete("{licitationId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteLicitation(Guid licitationId)
		{
			message.ServiceName = serviceName;
			message.Method = "DELETE";
			try
			{
				Licitation licitation = licitationRepository.GetLicitationById(licitationId);
				if (licitation == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of Licitation with identifier: " + licitationId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				licitationRepository.DeleteLicitation(licitationId);
				licitationRepository.SaveChanges(); //Perzistiramo promene
				message.Information = "Successfully deleted " + licitation.ToString();
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
		/// Licitation options
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetLicitationOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
