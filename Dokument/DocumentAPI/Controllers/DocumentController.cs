using AutoMapper;
using DocumentAPI.Data;
using DocumentAPI.Entities;
using DocumentAPI.Models;
using DocumentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAPI.Controllers
{
	[ApiController]
	[Route("api/document")]
	[Produces("application/json", "application/xml")]

	public class DocumentController : ControllerBase
	{
		private readonly IDocumentRepository documentRepository;
		private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
		private readonly IMapper mapper;

		private readonly ILoggerService loggerService;
		private readonly string serviceName = "DocumentAPI";
		private readonly Message message = new Message();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="documentRepository"></param>
		/// <param name="mapper"></param>
		/// <param name="loggerService"></param>
		/// <param name="linkGenerator"></param>
		public DocumentController(IDocumentRepository documentRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkGenerator)
		{
			this.documentRepository = documentRepository;
			this.mapper = mapper;
			this.loggerService = loggerService;
			this.linkGenerator = linkGenerator;
		}


		/// <summary>
		/// Get all documents
		/// </summary>
		/// <returns>List of documents</returns>
		[HttpGet]
		[HttpHead]
		[ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<List<DocumentDto>> GetDocuments()
		{
			List<DocumentEntity> documentEntities = documentRepository.GetDocuments();
			message.serviceName = serviceName;
			message.method = "GET";
			if (documentEntities == null || documentEntities.Count == 0)
			{
				message.information = "No content";
				message.error = "There is no content in database!";
				loggerService.CreateMessage(message);
				return NoContent();
			}
			message.information = "Returned list of Document";
			loggerService.CreateMessage(message);

			return Ok(mapper.Map<List<DocumentDto>>(documentEntities));

		}

		/// <summary>
		/// Get document by id
		/// </summary>
		/// <param name="documentID"></param>
		/// <returns></returns>

		[HttpGet("{documentID}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<DocumentDto> GetDocumentById(Guid documentID)
		{
			DocumentEntity document = documentRepository.GetDocumentById(documentID);
			message.serviceName = serviceName;
			message.method = "GET";

			if (document == null)
			{
				message.information = "Not found";
				message.error = "There is no object of Document with identifier: " + documentID;
				loggerService.CreateMessage(message);
				return NotFound();
			}
			message.information = document.ToString();
			loggerService.CreateMessage(message);

			return Ok(mapper.Map<DocumentDto>(document));
		}

		/// <summary>
		/// Create document
		/// </summary>
		/// <param name="documentDto"></param>
		/// <returns></returns>

		[HttpPost]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<DocumentConfirmationDto> CreateDocument([FromBody] DocumentCreationDto documentDto)
		{
			message.serviceName = serviceName;
			message.method = "POST";

			try
			{
				DocumentEntity document = mapper.Map<DocumentEntity>(documentDto);
				DocumentConfirmation confirmation = documentRepository.CreateDocument(document);
				documentRepository.SaveChanges();
				string location = linkGenerator.GetPathByAction("GetDocumentById", "Document", new { documentId = confirmation.documentId });

				message.information = document.ToString() + " | Document location: " + location;
				loggerService.CreateMessage(message);

				return Created(location, mapper.Map<DocumentConfirmationDto>(confirmation));
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
		/// Delete document
		/// </summary>
		/// <param name="documentID">Document id</param>
		/// <returns></returns>

		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{documentID}")]
		public IActionResult DeleteDocument(Guid documentID)
		{
			message.serviceName = serviceName;
			message.method = "DELETE";
			try
			{
				DocumentEntity doc = documentRepository.GetDocumentById(documentID);
				if (doc == null)
				{
					message.information = "Not found";
					message.error = "There is no object of Document with identifier: " + documentID;
					loggerService.CreateMessage(message);

					return NotFound();
				}


				documentRepository.DeleteDocument(documentID);
				documentRepository.SaveChanges();

				message.information = "Successfully deleted " + doc.ToString();
				return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + doc.ToString());
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
		/// Update document
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>

		[HttpPut]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<DocumentDto> UpdateDocument(DocumentEntity document)
		{
			message.serviceName = serviceName;
			message.method = "PUT";
			try
			{
				//Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
				var oldDocument = documentRepository.GetDocumentById(document.documentId);
				if (oldDocument == null)
				{
					message.information = "Not found";
					message.error = "There is no object of Document with identifier: " + document.documentId;
					loggerService.CreateMessage(message);
					return NotFound();
				}
				DocumentEntity doc = mapper.Map<DocumentEntity>(document);

				mapper.Map(doc, oldDocument); //Update objekta koji treba da sačuvamo u bazi                

				documentRepository.SaveChanges(); //Perzistiramo promene
				message.information = oldDocument.ToString();
				loggerService.CreateMessage(message);
				return Ok(mapper.Map<DocumentDto>(doc));
			}
			catch (Exception ex)
			{
				message.information = "Server error";
				message.error = ex.Message;
				loggerService.CreateMessage(message);
				return StatusCode(StatusCodes.Status500InternalServerError, "Error in edit");
			}
		}

		/// <summary>
		/// Options
		/// </summary>
		/// <returns></returns>
		[HttpOptions]
		public IActionResult GetDocumentOptions()
		{
			Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
			return Ok();
		}

	}
}
