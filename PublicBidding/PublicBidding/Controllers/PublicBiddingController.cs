using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicBidding.Data;
using PublicBidding.Models;
using PublicBidding.Services;

namespace PublicBidding.Controllers
{

	//Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
	[ApiController]
	[Route("api/publicBidding")]
	[Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
	public class PublicBiddingController : ControllerBase
	{
		private readonly IPublicBiddingRepository publicBiddingRepository;
		private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
		private readonly IMapper mapper;
		private readonly ILoggerService loggerService;
		private readonly string serviceName = "PublicBiddingService";
		private readonly Message message = new Message();
		private readonly IBuyerService buyerService;

		//Pomocu dependency injection-a dodajemo potrebne zavisnosti
		public PublicBiddingController(IPublicBiddingRepository publicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IBuyerService kupacService)
		{
			this.publicBiddingRepository = publicBiddingRepository;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
			this.loggerService = loggerService;
			this.buyerService = kupacService;
		}

		/// <summary>
		/// Get all public biddings
		/// </summary>
		/// <returns> List of public biddings</returns>
		/// <response code="200">List of public biddings</response>
		/// <response code="404">Not found any public bidding</response>
		[HttpGet]
		[HttpHead] // Returns only the header in the response
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<PublicBiddingDto>> GetPublicBidding()
		{
			List<Entities.PublicBidding>? publicBiddings = publicBiddingRepository.GetPublicBiddings();
			message.ServiceName = serviceName;
			message.Method = "GET";
			//if no public bidding is found, return status 204(NoContent)
			if (publicBiddings == null || publicBiddings.Count == 0)
			{
				message.Information = "No content";
				message.Error = "There is no content in database!";
				loggerService.CreateMessage(message);
				return NoContent();
			}

			List<PublicBiddingDto> publicBiddingDto = mapper.Map<List<PublicBiddingDto>>(publicBiddings);

			foreach (PublicBiddingDto j in publicBiddingDto)
			{
				j.buyer = buyerService.GetBestBidder(j.buyerId).Result;
			}

			message.Information = "Returned list of PublicBidding";
			loggerService.CreateMessage(message);

			// If a licitation is found, return the status 200 and the list of found public bidding
			return Ok(mapper.Map<List<PublicBiddingDto>>(publicBiddingDto));
		}

		/// <summary>
		/// Get public bidding by id
		/// </summary>
		/// // <param name="publicBiddingId">Public bidding id</param>
		/// <returns>Requested public bidding</returns>
		/// <response code="200">Get requested public bidding</response>
		/// <response code="404">Requested public bidding not found</response>
		[HttpGet("{publicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<PublicBiddingDto> GetPublicBidding(Guid publicBiddingId)
		{
			Entities.PublicBidding publicBidding = publicBiddingRepository.GetPublicBiddingById(publicBiddingId);
			message.ServiceName = serviceName;
			message.Method = "GET";
			if (publicBidding == null)
			{
				message.Information = "Not found";
				message.Error = "There is no object of PublicBidding with identifier: " + publicBiddingId;
				loggerService.CreateMessage(message);
				return NotFound();
			}

			PublicBiddingDto publicBiddingDto = mapper.Map<PublicBiddingDto>(publicBidding);
			publicBiddingDto.buyer = buyerService.GetBestBidder(publicBidding.buyerId).Result;

			message.Information = publicBidding.ToString();
			loggerService.CreateMessage(message);
			return Ok(publicBiddingDto);
		}

		/// <summary>
		/// Create public bidding
		/// </summary>
		/// /// <param name="publicBiddingDto">Public bidding model</param>
		/// <returns>Confirmation of the created public bidding.</returns>
		/// <remarks>
		/// Example of a request to create a new public bidding \
		/// POST /api/publicBidding \
		/// {   \
		///    "date": "2023-02-15T00:00:00", \
		///    "timeOfBeginning": "2023-02-15T07:00:00", \
		///    "timeOfEnd": "2023-02-15T10:00:00", \
		///    "initialPricePerHectare": 4000, \
		///    "excepted": false, \
		///    "typePublicBiddingId": "4246a611-7b2f-429d-a9ba-0e539c81b82f", \
		///    "auctionedPrice": 6000, \
		///    "leasePeriod": 12, \
		///    "numberOfParticipants": 10, \
		///    "depositTopUpAmount": 400, \
		///    "circle": 1, \
		///    "statusOfPublicBiddingId": "8aaa90c8-56f3-4a76-b07a-f895eded5a84", \
		///    "addressId": "a06f99d2-0ba7-40ff-a241-304a03dfe4be", \
		///    "parcelsId" = new List<Guid>() { Guid.Parse("B823F2AC-0022-4758-ABD9-2BDC6A36BF95") }, \
		///    "authorizedBidderPersonId": "5cfa282f-8324-4a8b-8c23-8d43502ca01e", \
		///    "buyerId": "8b3b7775-4293-4b41-9ccc-19f9cf694d68", \
		///    "buyersId" = new List<Guid>() { Guid.Parse("F72EC03D-0B58-4A90-909A-DF79BA497EC1") }, \
		///    "userId" = Guid.Parse("7836E78D-26D4-441D-843F-21062CDA2240") \
		/// }
		/// </remarks>
		/// <response code="201">Get created public bidding</response>
		/// <response code="500">An error occurred on the server while creating the public bidding</response>
		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<PublicBiddingConfirmationDto> CreatePublicBidding([FromBody] PublicBiddingCreationDto publicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "POST";
			try
			{
				Entities.PublicBidding publicBidding = mapper.Map<Entities.PublicBidding>(publicBiddingDto);
				PublicBiddingConfirmationDto j = publicBiddingRepository.CreatePublicBidding(publicBidding);
				publicBiddingRepository.SaveChanges(); 
														 //generisati identifikator novokreiranog resursa
				string? location = linkGenerator.GetPathByAction("GetPublicBidding", "PublicBidding", new { publicBiddingId = j.publicBiddingId });
				message.Information = publicBiddingDto.ToString() + " | PublicBidding location: " + location;
				loggerService.CreateMessage(message);
				return Created(location, mapper.Map<PublicBiddingConfirmationDto>(j));
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
		/// Update public bidding
		/// </summary>
		/// <param name="publicBiddingDto">Public bidding model to be updated</param>
		/// <returns>Confirmation of updated public bidding</returns>
		/// <response code="200">Get updated public bidding</response>
		/// <response code="400">The public bidding to be updated was not found</response>
		/// <response code="500">An error occurred while updating the public bidding</response>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<PublicBiddingConfirmationDto> UpdatePublicBidding(PublicBiddingUpdateDto publicBiddingDto)
		{
			message.ServiceName = serviceName;
			message.Method = "PUT";
			try
			{
				var old = publicBiddingRepository.GetPublicBiddingById(publicBiddingDto.publicBiddingId);
				if (old == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of PublicBidding with identifier: " + publicBiddingDto.publicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				Entities.PublicBidding publicBidding = mapper.Map<Entities.PublicBidding>(publicBiddingDto);
				mapper.Map(publicBidding, old);
				publicBiddingRepository.SaveChanges(); //Perzistiramo promene
				message.Information = old.ToString();
				loggerService.CreateMessage(message);
				return Ok(mapper.Map<PublicBiddingConfirmationDto>(old));
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
		/// Delete public bidding
		/// </summary>
		/// <param name="publicBiddingId">Public bidding id</param>
		/// <returns>Status 204 (NoContent)</returns>
		/// <response code="204">>Public bidding deleted</response>
		/// <response code="404">No public bidding found to delete</response>
		/// <response code="500">An error occurred on the server while deleting the public bidding</response>
		[HttpDelete("{publicBiddingId}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeletePublicBidding(Guid publicBiddingId)
		{
			message.ServiceName = serviceName;
			message.Method = "DELETE";
			try
			{
				Entities.PublicBidding publicBidding = publicBiddingRepository.GetPublicBiddingById(publicBiddingId);
				if (publicBidding == null)
				{
					message.Information = "Not found";
					message.Error = "There is no object of PublicBidding with identifier: " + publicBiddingId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				publicBiddingRepository.DeletePublicBidding(publicBiddingId);
				publicBiddingRepository.SaveChanges(); //Perzistiramo promene
				message.Information = "Successfully deleted " + publicBidding.ToString();
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
		/// Public bidding options
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetPublicBiddingOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
