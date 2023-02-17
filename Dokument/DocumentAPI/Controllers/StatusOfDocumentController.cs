using AutoMapper;
using DocumentAPI.Data;
using DocumentAPI.Entities;
using DocumentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAPI.Controllers
{
	[ApiController]
	[Route("api/statusOfDocument")]
	[Produces("application/json", "application/xml")]

	public class StatusOfDocumentController : ControllerBase
	{

		private readonly IStatusOfDocumentRepository statusOfDocumentRepository;
		private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
		private readonly IMapper mapper;

		/// <summary>
		/// Construstor
		/// </summary>
		/// <param name="statusOfDocumentRepository"></param>
		/// <param name="linkGenerator"></param>
		/// <param name="mapper"></param>
		public StatusOfDocumentController(IStatusOfDocumentRepository statusOfDocumentRepository, LinkGenerator linkGenerator, IMapper mapper)
		{
			this.statusOfDocumentRepository = statusOfDocumentRepository;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
		}

		/// <summary>
		/// Get all document statuses
		/// </summary>
		/// <param name="adopted"></param>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<StatusOfDocumentDto>> GetStatusOfDocumentEntities(bool adopted = false)
		{
			List<StatusOfDocumentEntity> statusOfDocumentEntities = statusOfDocumentRepository.GetStatusOfDocumentEntities(adopted);
			if (statusOfDocumentEntities == null || statusOfDocumentEntities.Count == 0)
			{
				return NoContent();
			}
			return Ok(mapper.Map<List<StatusOfDocumentDto>>(statusOfDocumentEntities));

		}


		/// <summary>
		/// Get document statuses by id
		/// </summary>
		/// <param name="statusOfDocumentId"></param>
		/// <returns></returns>

		[HttpGet("{statusOfDocumentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<StatusOfDocumentDto> GetStatusOfDocumentById(Guid statusOfDocumentId)
		{
			StatusOfDocumentEntity document = statusOfDocumentRepository.GetStatusOfDocumentById(statusOfDocumentId);

			if (document == null)
			{
				return NotFound();
			}
			return Ok(mapper.Map<StatusOfDocumentDto>(document));
		}

		/// <summary>
		/// Create document statuses
		/// </summary>
		/// <param name="documentDto"></param>
		/// <returns></returns>

		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<StatusOfDocumentEntity> CreateStatusOfDocument([FromBody] StatusOfDocumentCreationDto documentDto)
		{
			try
			{
				var document = mapper.Map<StatusOfDocumentEntity>(documentDto);
				var confirmation = statusOfDocumentRepository.CreateStatusOfDocument(document);
				statusOfDocumentRepository.SaveChanges();
				string location = linkGenerator.GetPathByAction("GetStatusOfDocumentById", "StatusOfDocument", new { statusOfDocumentId = confirmation.statusOfDocumentId });

				return Created(location, mapper.Map<StatusOfDocumentConfirmationDto>(confirmation));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}



		/// <summary>
		/// Delete document statuses
		/// </summary>
		/// <param name="statusOfDocumentId"></param>
		/// <returns></returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{statusOfDocumentId}")]
		public IActionResult DeleteStatusOfDocumentId(Guid statusOfDocumentId)
		{
			try
			{
				StatusOfDocumentEntity doc = statusOfDocumentRepository.GetStatusOfDocumentById(statusOfDocumentId);
				if (doc == null)
				{
					return NotFound();
				}
				statusOfDocumentRepository.DeleteStatusOfDocument(statusOfDocumentId);
				statusOfDocumentRepository.SaveChanges();
				return NoContent();
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
			}
		}


		/// <summary>
		/// Update document statuses
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<StatusOfDocumentDto> UpdateStatusOfDocument(StatusOfDocumentEntity document)
		{
			try
			{
				var oldDocument = statusOfDocumentRepository.GetStatusOfDocumentById((Guid)document.statusOfDocumentId);
				if (oldDocument == null)
				{
					return NotFound(); 
				}
				StatusOfDocumentEntity doc = mapper.Map<StatusOfDocumentEntity>(document);

				mapper.Map(doc, oldDocument);          

				statusOfDocumentRepository.SaveChanges(); 
				return Ok(mapper.Map<StatusOfDocumentDto>(doc));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
			}
		}


		/// <summary>
		/// Options
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetStatusOfDocumentOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
