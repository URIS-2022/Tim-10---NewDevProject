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
	[Route("api/typeOfPublicBidding")]
	[Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate

	public class TypeOfPublicBiddingController : ControllerBase
	{
		private readonly ITypeOfPublicBiddingRepository typeOfPublicBiddingRepository;
		private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
		private readonly IMapper mapper;
		private readonly ILoggerService loggerService;
		private readonly string serviceName = "PublicBiddingService";
		private readonly Message message = new Message();

		//Pomocu dependency injection-a dodajemo potrebne zavisnosti
		public TypeOfPublicBiddingController(ITypeOfPublicBiddingRepository typeOfPublicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
		{
			this.typeOfPublicBiddingRepository = typeOfPublicBiddingRepository;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
			this.loggerService = loggerService;
		}

		/// <summary>
		/// Get all types of public bidding by id
		/// </summary>
		/// <returns> List of types of public bidding by id</returns>
		/// <response code="200">List of types of public bidding by id</response>
		/// <response code="404">Not found any type of public bidding by id</response>
		[HttpGet]
		[HttpHead] // Returns only the header in the response
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<TypeOfPublicBiddingDto>> GetTypesOfPublicBidding()
		{
			List<TypeOfPublicBidding> typesOfPublicBidding = typeOfPublicBiddingRepository.GetTypesOfPublicBidding();
			//if no type of public bidding by id is found, return status 204(NoContent)
			message.ServiceName = serviceName;
			message.Method = "GET";
			if (typesOfPublicBidding == null || typesOfPublicBidding.Count == 0)
			{
				message.Information = "No content";
				message.Error = "There is no content in database!";
				loggerService.CreateMessage(message);
				return NoContent();
			}

			message.Information = "Returned list of TypeOfPublicBidding";
			loggerService.CreateMessage(message);
			// If a licitation is found, return the status 200 and the list of found type of public bidding by id
			return Ok(mapper.Map<List<TypeOfPublicBiddingDto>>(typesOfPublicBidding));
		}

		/// <summary>
		/// Get type of public bidding by id
		/// </summary>
		/// // <param name="typePublicBiddingId">Type of public bidding id</param>
		/// <returns>Requested type of public bidding</returns>
		/// <response code="200">Get requested type of public bidding</response>
		/// <response code="404">Requested type of public bidding not found</response>
		[HttpGet("{typePublicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<TypeOfPublicBiddingDto> GetTypeOfPublicBidding(Guid typePublicBiddingId)
		{
			TypeOfPublicBidding? typeOfPublicBidding = typeOfPublicBiddingRepository.GetTypeOfPublicBiddingById(typePublicBiddingId);
			message.ServiceName = serviceName;
			message.Method = "GET";
			if (typeOfPublicBidding == null)
			{
				message.Information = "Not found";
				message.Error = "There is no object of TypeOfPublicBidding with identifier: " + typePublicBiddingId;
				loggerService.CreateMessage(message);
				return NotFound();
			}
			message.Information = typeOfPublicBidding.ToString();
			loggerService.CreateMessage(message);
			return Ok(mapper.Map<TypeOfPublicBiddingDto>(typeOfPublicBidding));
		}

		/// <summary>
		/// Create type of public bidding
		/// </summary>
		/// /// <param name="typeOfPublicBiddingDto">Type of public bidding mode</param>
		/// <returns>Confirmation of the created type of public bidding.</returns>
		/// <remarks>
		/// Example of a request to create a new type of public bidding \
		/// POST /api/typeOfPublicBidding \
		/// {   \
		///    "typePublicBiddingName": "New type" \
		/// }
		/// </remarks>
		/// <response code="201">Get created type of public bidding</response>
		/// <response code="500">An error occurred on the server while creating the type of public bidding</response>
		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<TypeOfPublicBiddingConfirmationDto> CreateTypeOfPublicBidding([FromBody] TypeOfPublicBiddingCreationDto typeOfPublicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "POST";
			try
			{
				TypeOfPublicBidding type = mapper.Map<TypeOfPublicBidding>(typeOfPublicBiddingDto);
				TypeOfPublicBiddingConfirmationDto t = typeOfPublicBiddingRepository.CreateTypeOfPublicBidding(type);
				typeOfPublicBiddingRepository.SaveChanges(); //Perzistiramo promene
															 //generisati identifikator novokreiranog resursa
				string? location = linkGenerator.GetPathByAction("GetTypeOfPublicBidding", "TypeOfPublicBidding", new { typePublicBiddingId = t.typePublicBiddingId });
				message.Information = typeOfPublicBiddingDto.ToString() + " | TypeOfPublicBidding location: " + location;
				loggerService.CreateMessage(message);
				return Created(location, mapper.Map<TypeOfPublicBiddingConfirmationDto>(t));
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
		/// Update type of public bidding
		/// </summary>
		/// <param name="typeOfPublicBiddingDto">Type of public bidding model to be updated</param>
		/// <returns>Confirmation of updated type of public bidding</returns>
		/// <response code="200">Get updated type of public bidding</response>
		/// <response code="400">The type of public bidding to be updated was not found</response>
		/// <response code="500">An error occurred while updating the type of public bidding</response>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<TypeOfPublicBiddingConfirmationDto> UpdateTypeOfPublicBidding(TypeOfPublicBiddingUpdateDto typeOfPublicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "PUT";
			try
			{
				var starije = typeOfPublicBiddingRepository.GetTypeOfPublicBiddingById(typeOfPublicBiddingDto.typePublicBiddingId);
				// Checking if the type we want to update exists
				if (starije == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of TypeOfPublicBidding with identifier: " + typeOfPublicBiddingDto.typePublicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
				}
				TypeOfPublicBidding typeOfPublicBidding = mapper.Map<TypeOfPublicBidding>(typeOfPublicBiddingDto);
				mapper.Map(typeOfPublicBidding, starije);
				typeOfPublicBiddingRepository.SaveChanges(); 
				message.Information = starije.ToString();
				loggerService.CreateMessage(message);
				return Ok(mapper.Map<TypeOfPublicBiddingConfirmationDto>(starije));
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
		/// Delete type of public bidding
		/// </summary>
		/// <param name="typePublicBiddingId">Type of public bidding id</param>
		/// <returns>Status 204 (NoContent)</returns>
		/// <response code="204">Type of public bidding deleted</response>
		/// <response code="404">No type of public bidding found to delete</response>
		/// <response code="500">An error occurred while deleting the type of public bidding</response>
		[HttpDelete("{typePublicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteTypeOfPublicBidding(Guid typePublicBiddingId)
		{
			message.ServiceName = serviceName;
			message.Method = "DELETE";
			try
			{
				TypeOfPublicBidding typeOfPublicBidding = typeOfPublicBiddingRepository.GetTypeOfPublicBiddingById(typePublicBiddingId);
				if (typeOfPublicBidding == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of TypeOfPublicBidding with identifier: " + typePublicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				typeOfPublicBiddingRepository.DeleteTypeOfPublicBidding(typePublicBiddingId);
				typeOfPublicBiddingRepository.SaveChanges(); //Perzistiramo promene
				message.Information = "Successfully deleted " + typeOfPublicBidding.ToString();
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
		/// Type of public bidding options
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetTypeOfPublicBiddingOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
