using AutoMapper;
using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public class StatusOfDocumentRepository : IStatusOfDocumentRepository
	{
		private readonly DocumentContext context;
		private readonly IMapper mapper;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context"></param>
		/// <param name="mapper"></param>
		public StatusOfDocumentRepository(DocumentContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		/// <summary>
		/// Save changes
		/// </summary>
		/// <returns></returns>

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		/// <summary>
		/// Get all status of documents
		/// </summary>
		/// <param name="adopted"></param>
		/// <returns></returns>

		public List<StatusOfDocumentEntity> GetStatusOfDocumentEntities(bool adopted = false)
		{
			return context.StatusOfDocumentEntity.Where(e => (adopted == false)).ToList();
		}


		/// <summary>
		/// Get status of document by id
		/// </summary>
		/// <param name="statusOfDocumentId"></param>
		/// <returns></returns>

		public StatusOfDocumentEntity GetStatusOfDocumentById(Guid statusOfDocumentId)
		{
			return context.StatusOfDocumentEntity.FirstOrDefault(e => e.statusOfDocumentId == statusOfDocumentId);
		}


		/// <summary>
		/// Create status of document
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>
		public StatusOfDocumentConfirmation CreateStatusOfDocument(StatusOfDocumentEntity document)
		{
			var createdEntity = context.Add(document);
			return mapper.Map<StatusOfDocumentConfirmation>(createdEntity.Entity);
		}

		/// <summary>
		/// Update status of document
		/// </summary>
		/// <param name="document"></param>

		public void UpdateStatusOfDocument(StatusOfDocumentEntity document)
		{
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
		}

		/// <summary>
		/// Delete status of document
		/// </summary>
		/// <param name="statusOfDocumentId"></param>

		public void DeleteStatusOfDocument(Guid statusOfDocumentId)
		{
			var document = GetStatusOfDocumentById(statusOfDocumentId);
			context.Remove(document);
		}

	}
}
