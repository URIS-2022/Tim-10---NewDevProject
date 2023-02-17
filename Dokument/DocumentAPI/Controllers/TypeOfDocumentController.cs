using AutoMapper;
using DocumentAPI.Data;
using DocumentAPI.Entities;
using DocumentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAPI.Controllers
{

	[ApiController]
	[Route("api/typeOfDocument")]
	[Produces("application/json", "application/xml")]

	public class TypeOfDocumentController : ControllerBase
	{
		private readonly ITypeOfDocumentRepository typeOfDocument;
		private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
		private readonly IMapper mapper;

		/// <summary>
		/// Construstor
		/// </summary>
		/// <param name="typeOfDocumentRepository"></param>
		/// <param name="linkGenerator"></param>
		/// <param name="mapper"></param>
		public TypeOfDocumentController(ITypeOfDocumentRepository typeOfDocument, LinkGenerator linkGenerator, IMapper mapper)
		{
			this.typeOfDocument = typeOfDocument;
			this.linkGenerator = linkGenerator;
			this.mapper = mapper;
		}


		/// <summary>
		/// Get document type by name
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<TypeOfDocumentDto>> GetTypeOfDocumentEntities()
		{
			List<TypeOfDocumentEntity> docType = typeOfDocument.GetTypeOfDocumentEntities();
			if (docType == null || docType.Count == 0)
			{
				return NoContent();
			}
			return Ok(mapper.Map<List<TypeOfDocumentDto>>(docType));

		}

		/// <summary>
		/// Get document type by id
		/// </summary>
		/// <param name="typeOfDocumentId"></param>
		/// <returns></returns>

		[HttpGet("{typeOfDocumentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<TypeOfDocumentDto> GetTypeOfDocumentById(Guid typeOfDocumentId)
		{
			TypeOfDocumentEntity document = this.typeOfDocument.GetTypeOfDocumentById(typeOfDocumentId);

			if (document == null)
			{
				return NotFound();
			}
			return Ok(mapper.Map<TypeOfDocumentDto>(document));
		}


		/// <summary>
		/// Create type of document
		/// <returns></returns>
		/// <param name="typeOfDocumentDto">Model javnog nadmetanja</param>

		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<TypeOfDocumentEntity> CreateTypeOfDocument([FromBody] TypeOfDocumentCreationDto typeOfDocumentDto)
		{
			try
			{
				TypeOfDocumentEntity documentType = mapper.Map<TypeOfDocumentEntity>(typeOfDocumentDto);
				var confirmation = typeOfDocument.CreateTypeOfDocument(documentType);
				typeOfDocument.SaveChanges(); //Perzistiramo promene
				string location = linkGenerator.GetPathByAction("GetTypeOfDocumentById", "TypeOfDocument", new { typeOfDocumentId = confirmation.typeOfDocumentId });

				return Created(location, mapper.Map<TypeOfDocumentConfirmationDto>(confirmation));
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Delete document type
		/// </summary>
		/// <param name="typeOfDocumentId"></param>
		/// <returns></returns>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{typeOfDocumentId}")]
		public IActionResult DeleteTypeOfDocument(Guid typeOfDocumentId)
		{
			try
			{
				TypeOfDocumentEntity docType = typeOfDocument.GetTypeOfDocumentById(typeOfDocumentId);
				if (docType == null)
				{
					return NotFound();
				}
				typeOfDocument.DeleteTypeOfDocument(typeOfDocumentId);
				typeOfDocument.SaveChanges();
				return NoContent();
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
			}
		}


		/// <summary>
		/// Update type of document
		/// </summary>
		/// <param name="typeOfDocument"></param>
		/// <returns></returns>
		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<TypeOfDocumentDto> UpdateTypeOfDocument(TypeOfDocumentEntity typeOfDocument)
		{
			try
			{
				var oldTypeOfDocument = this.typeOfDocument.GetTypeOfDocumentById(typeOfDocument.typeOfDocumentId);
				if (oldTypeOfDocument == null)
				{
					return NotFound(); 
				}
				TypeOfDocumentEntity docType = mapper.Map<TypeOfDocumentEntity>(typeOfDocument);

				mapper.Map(docType, oldTypeOfDocument);               

				this.typeOfDocument.SaveChanges(); 
				return Ok(mapper.Map<TypeOfDocumentEntity>(docType));
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
		public IActionResult GetTypeOfDocumentOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
